using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using WorkBlockApi.Interfaces;
using WorkBlockApi.Models;
using WorkBlockApi.SmartContractsDefinitions.PontoBlock;
using WorkBlockApi.SmartContractsDefinitions.PontoBlock.ContractDefinition;
using WorkBlockApi.SmartContractsDefinitions.UtilContract;
using WorkBlockApi.ViewModels;

namespace WorkBlockApi.Controllers.Contracts;

[ApiController]
[Route("v1/contracts/pontoblock")]
public class PontoBlockController : ControllerBase
{
    private readonly Web3 _web3;
    private readonly IContractModelRepository _contractModelRepository;
    private readonly IMemoryCache _memoryCache;
    private const string ContractName = "PontoBlock";

    private ContractModel? PontoBlock { get; }

    public PontoBlockController(
        IContractModelRepository contractModelRepository,
        ApiConfiguration configuration,
        IMemoryCache memoryCache)
    {
        _web3 = new Web3(new Account(configuration.PrivateKey), configuration.Provider);
        _contractModelRepository = contractModelRepository;
        _memoryCache = memoryCache;
        PontoBlock = GetContractsInMemory()!.Result.FirstOrDefault(x => x.Name == ContractName);
    }

    [HttpGet("GetCreationDate")]
    public async Task<IActionResult> GetCreationDateContract()
    {
        try
        {
            if (PontoBlock == null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            var service = new PontoBlockService(_web3, PontoBlock.AddressContract);

            var result = await service.GetCreationDateContractQueryAsync();

            return StatusCode(200, new ResultViewModel<ulong>((ulong)result));
        }
        catch (SmartContractRevertException e)
        {
            return StatusCode(500, new ResultViewModel<string>(e.RevertMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<string>(e.Message));
        }
    }

    [HttpGet("GetMoment")]
    public async Task<IActionResult> GetMoment()
    {
        try
        {
            if (PontoBlock == null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            var service = new PontoBlockService(_web3, PontoBlock.AddressContract);

            var result = await service.GetMomentQueryAsync();

            return StatusCode(200, new ResultViewModel<ulong>((ulong)result));
        }
        catch (SmartContractRevertException e)
        {
            return StatusCode(500, new ResultViewModel<string>(e.RevertMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<string>(e.Message));
        }
    }

    [HttpGet("GetOwner")]
    public async Task<IActionResult> GetOwner()
    {
        try
        {
            if (PontoBlock == null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            var service = new PontoBlockService(_web3, PontoBlock.AddressContract);

            var result = await service.GetOwnerQueryAsync();

            return StatusCode(200, new ResultViewModel<string>(result, new List<string>()));
        }
        catch (SmartContractRevertException e)
        {
            return StatusCode(500, new ResultViewModel<string>(e.RevertMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<string>(e.Message));
        }
    }

    [HttpPost("ChangeOwner")]
    public async Task<IActionResult> ChangeOwner(string newAddress)
    {
        try
        {
            if (PontoBlock == null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            if (string.IsNullOrEmpty(newAddress))
                return NotFound(new ResultViewModel<string>("NewAddress is required"));

            var service = new PontoBlockService(_web3, PontoBlock.AddressContract);

            var result = await service.ChangeOwnerRequestAsync(newAddress);

            return StatusCode(200, new ResultViewModel<string>($"Success. Transaction {result}"));
        }
        catch (SmartContractRevertException e)
        {
            return StatusCode(500, new ResultViewModel<string>(e.RevertMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<string>(e.Message));
        }
    }

    [HttpGet("GetTimeZone")]
    public async Task<IActionResult> GetTimeZone()
    {
        try
        {
            if (PontoBlock == null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            var service = new PontoBlockService(_web3, PontoBlock.AddressContract);

            var result = await service.GetTimeZoneQueryAsync();

            return StatusCode(200, new ResultViewModel<int>((int)result));
        }
        catch (SmartContractRevertException e)
        {
            return StatusCode(500, new ResultViewModel<string>(e.RevertMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<string>(e.Message));
        }
    }

    [HttpGet("GetEmployeeRecords")]
    public async Task<IActionResult> GetEmployeeRecords(string address, int timestamp)
    {
        try
        {
            if (PontoBlock == null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            if (string.IsNullOrEmpty(address))
                return NotFound(new ResultViewModel<string>("Address is required"));

            if (!await IsValidTimestamp(timestamp))
                return NotFound(new ResultViewModel<string>("Invalid timestamp"));

            var service = new PontoBlockService(_web3, PontoBlock.AddressContract);

            var date = await ValidateDateAsync(timestamp);
            if (date == null)
            {
                return NotFound(new ResultViewModel<string>("Invalid timestamp"));
            }

            var result = await service.GetEmployeeRecordsQueryAsync(address, (ulong)date);

            var employeeRecord = new EmployeeRecord
            {
                StartWork = DateTimeOffset.FromUnixTimeSeconds((int)result.ReturnValue1.StartWork).UtcDateTime,
                BreakStartTime = DateTimeOffset.FromUnixTimeSeconds((int)result.ReturnValue1.BreakStartTime).UtcDateTime,
                BreakEndTime = DateTimeOffset.FromUnixTimeSeconds((int)result.ReturnValue1.BreakEndTime).UtcDateTime,
                EndWork = DateTimeOffset.FromUnixTimeSeconds((int)result.ReturnValue1.EndWork).UtcDateTime
            };

            return StatusCode(200, new ResultViewModel<EmployeeRecord>(employeeRecord));
        }
        catch (SmartContractRevertException e)
        {
            return StatusCode(500, new ResultViewModel<string>(e.RevertMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<string>(e.Message));
        }
    }

    private Task<List<ContractModel>>? GetContractsInMemory()
    {
        return
            _memoryCache.GetOrCreate("ContractsCache", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                return GetContractsAsync();
            });
    }

    private async Task<List<ContractModel>> GetContractsAsync()
    {
        return
            await
                _contractModelRepository.GetAllContractsAsync();
    }

    private async Task<ulong?> ValidateDateAsync(int timestamp)
    {
        try
        {
            var utilContract = GetContractsInMemory()!.Result.FirstOrDefault(x => x.Name == "UtilContract");
            if (utilContract == null)
                return null;

            var utilService = new UtilContractService(_web3, utilContract.AddressContract);

            var result = await utilService.GetDateQueryAsync(timestamp);

            return (ulong)result;
        }
        catch (Exception e)
        {
            return null;
        }

    }

    private async Task<ulong?> GetCreationDateContractAsync()
    {
        try
        {
            var pontoBlock = GetContractsInMemory()!.Result.FirstOrDefault(x => x.Name == "PontoBlock");
            if (pontoBlock == null)
                return null;

            var pontoBlockService = new PontoBlockService(_web3, pontoBlock.AddressContract);

            var result = await pontoBlockService.GetCreationDateContractQueryAsync();

            return (ulong)result;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    private async Task<bool> IsValidTimestamp(int timestamp)
    {
        if (timestamp <= 0)
            return false;

        var reference = await ValidateDateAsync(timestamp);
        var contractDate = await GetCreationDateContractAsync();

        if (contractDate == null || reference == null)
            return false;

        return !(reference < contractDate);
    }
}