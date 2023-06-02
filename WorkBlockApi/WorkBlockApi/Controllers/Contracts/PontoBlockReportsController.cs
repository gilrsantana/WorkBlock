using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using WorkBlockApi.Interfaces;
using WorkBlockApi.Models;
using WorkBlockApi.SmartContractsDefinitions.PontoBlock;
using WorkBlockApi.SmartContractsDefinitions.PontoBlockReports;
using WorkBlockApi.SmartContractsDefinitions.PontoBlockReports.ContractDefinition;
using WorkBlockApi.SmartContractsDefinitions.UtilContract;
using WorkBlockApi.ViewModels;

namespace WorkBlockApi.Controllers.Contracts;

[ApiController]
[Route("v1/contracts/pontoblockreports")]
public class PontoBlockReportsController : ControllerBase
{
    private readonly Web3 _web3;
    private readonly IContractModelRepository _contractModelRepository;
    private readonly IMemoryCache _memoryCache;
    private const string ContractName = "PontoBlockReports";

    private ContractModel? PontoBlockReports { get; }

    public PontoBlockReportsController(
        IContractModelRepository contractModelRepository,
        ApiConfiguration configuration,
        IMemoryCache memoryCache)
    {
        _web3 = new Web3(new Account(configuration.PrivateKey), configuration.Provider);
        _contractModelRepository = contractModelRepository;
        _memoryCache = memoryCache;
        PontoBlockReports = GetContractsInMemory()!.Result.FirstOrDefault(x => x.Name == ContractName);
    }

    [HttpGet("GetWorkTimesFromEmployeeAtDate")]
    public async Task<IActionResult> GetWorkTimesFromEmployeeAtDateAsync(string address, int timestamp)
    {
        try
        {
            if (PontoBlockReports == null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            if (await IsValidTimestamp(timestamp))
                return NotFound(new ResultViewModel<string>("Invalid timestamp"));

            var service = new PontoBlockReportsService(_web3, PontoBlockReports.AddressContract);

            var date = await ValidateDateAsync(timestamp);
            if (date == null)
            {
                return NotFound(new ResultViewModel<string>("Invalid timestamp"));
            }

            var result = await service.GetWorkTimesFromEmployeeAtDateQueryAsync(address, (ulong)date);

            return StatusCode(200, new ResultViewModel<GetWorkTimesFromEmployeeAtDateOutputDTO>(result));
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

    [HttpGet("GetWorkTimeFromEmployeeBetweenTwoDates")]
    public async Task<IActionResult> GetWorkTimeFromEmployeeBetweenTwoDates(string address, int startTimestamp, int endTimestamp)
    {
        try
        {
            if (PontoBlockReports == null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            if (await IsValidTimestamp(startTimestamp))
                return NotFound(new ResultViewModel<string>("Invalid startTimestamp"));

            if (await IsValidTimestamp(endTimestamp))
                return NotFound(new ResultViewModel<string>("Invalid endTimestamp"));

            var service = new PontoBlockReportsService(_web3, PontoBlockReports.AddressContract);

            var startDate = await ValidateDateAsync(startTimestamp);
            if (startDate == null)
                return NotFound(new ResultViewModel<string>("Invalid startTimestamp"));

            var endDate = await ValidateDateAsync(endTimestamp);
            if (endDate == null)
                return NotFound(new ResultViewModel<string>("Invalid endTimestamp"));

            var result = await service.GetWorkTimeFromEmployeeBetweenTwoDatesQueryAsync(address, (ulong)startDate, (ulong) endDate);

            return StatusCode(200, new ResultViewModel<GetWorkTimeFromEmployeeBetweenTwoDatesOutputDTO>(result));
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

    [HttpGet("GetWorkTimesForAllEmployeesAtDate")]
    public async Task<IActionResult> GetWorkTimesForAllEmployeesAtDate(int timestamp)
    {
        try
        {
            if (PontoBlockReports == null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            if (await IsValidTimestamp(timestamp))
                return NotFound(new ResultViewModel<string>("Invalid timestamp"));

            var service = new PontoBlockReportsService(_web3, PontoBlockReports.AddressContract);

            var date = await ValidateDateAsync(timestamp);
            if (date == null)
                return NotFound(new ResultViewModel<string>("Invalid timestamp"));

            var result = await service.GetWorkTimesForAllEmployeesAtDateQueryAsync((ulong)date);

            return StatusCode(200, new ResultViewModel<GetWorkTimesForAllEmployeesAtDateOutputDTO>(result));
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

    [HttpGet("getWorkTimesForAllEmployeesBetweenTwoDates")]
    public async Task<IActionResult> GetWorkTimesForAllEmployeesBetweenTwoDates(int startTimestamp, int endTimestamp)
    {
        try
        {
            if (PontoBlockReports == null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            if (await IsValidTimestamp(startTimestamp))
                return NotFound(new ResultViewModel<string>("Invalid startTimestamp"));

            if (await IsValidTimestamp(endTimestamp))
                return NotFound(new ResultViewModel<string>("Invalid endTimestamp"));

            var service = new PontoBlockReportsService(_web3, PontoBlockReports.AddressContract);

            var startDate = await ValidateDateAsync(startTimestamp);
            if (startDate == null)
                return NotFound(new ResultViewModel<string>("Invalid startTimestamp"));

            var endDate = await ValidateDateAsync(endTimestamp);
            if (endDate == null)
                return NotFound(new ResultViewModel<string>("Invalid endTimestamp"));

            var result = await service.GetWorkTimesForAllEmployeesBetweenTwoDatesQueryAsync((ulong)startDate, (ulong) endDate);

            return StatusCode(200, new ResultViewModel<GetWorkTimesForAllEmployeesBetweenTwoDatesOutputDTO>(result));
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

        return await ValidateDateAsync(timestamp) != null &&
               await GetCreationDateContractAsync() != null &&
               !(await ValidateDateAsync(timestamp) < await GetCreationDateContractAsync());
    }
}