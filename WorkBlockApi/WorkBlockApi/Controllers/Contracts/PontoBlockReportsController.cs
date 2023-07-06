using System.Linq;
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
        PontoBlockReports = GetContractsInMemory()!.Result.Find(x => x.Name == ContractName);
    }

    [HttpGet("GetWorkTimesFromEmployeeAtDate")]
    public async Task<IActionResult> GetWorkTimesFromEmployeeAtDateAsync(string address, int timestamp)
    {
        try
        {
            if (PontoBlockReports == null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            if (!await IsValidTimestamp(timestamp))
                return NotFound(new ResultViewModel<string>("Invalid timestamp"));

            var service = new PontoBlockReportsService(_web3, PontoBlockReports.AddressContract);

            var date = await ValidateDateAsync(timestamp);
            if (date == null)
                return NotFound(new ResultViewModel<string>("Invalid timestamp"));

            var data = await service.GetWorkTimesFromEmployeeAtDateQueryAsync(address, (ulong)date);

            var result = new EmployeeDateViewModel
            {
                Date = new List<ulong> { (ulong)date },
                Address = new List<List<string>> { new List<string> { address } },
                StartWork = new List<List<ulong>> { new List<ulong> { (ulong)data.StartWork } },
                EndWork = new List<List<ulong>> { new List<ulong> { (ulong)data.EndWork } },
                BreakStartTime = new List<List<ulong>> { new List<ulong> { (ulong)data.BreakStartTime } },
                BreakEndTime = new List<List<ulong>> { new List<ulong> { (ulong)data.BreakEndTime } }
            };

            return StatusCode(200, new ResultViewModel<EmployeeDateViewModel>(result));
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

            if (!await IsValidTimestamp(startTimestamp))
                return NotFound(new ResultViewModel<string>("Invalid startTimestamp"));

            if (!await IsValidTimestamp(endTimestamp))
                return NotFound(new ResultViewModel<string>("Invalid endTimestamp"));

            var service = new PontoBlockReportsService(_web3, PontoBlockReports.AddressContract);

            var startDate = await ValidateDateAsync(startTimestamp);
            if (startDate == null)
                return NotFound(new ResultViewModel<string>("Invalid startTimestamp"));

            var endDate = await ValidateDateAsync(endTimestamp);
            if (endDate == null)
                return NotFound(new ResultViewModel<string>("Invalid endTimestamp"));

            var data = await service.GetWorkTimeFromEmployeeBetweenTwoDatesQueryAsync(address, (ulong)startDate, (ulong) endDate);

            var result = new EmployeeDateViewModel
            {
                Date = data.Date.Select(bigInteger => (ulong)bigInteger).ToList(),
                Address = new List<List<string>> { new List<string> { address } },
                StartWork = new List<List<ulong>> { data.StartWork.Select(bigInteger => (ulong)bigInteger).ToList() },
                EndWork = new List<List<ulong>> { data.EndWork.Select(bigInteger => (ulong)bigInteger).ToList() },
                BreakStartTime = new List<List<ulong>> { data.BreakStartTime.Select(bigInteger => (ulong)bigInteger).ToList() },
                BreakEndTime = new List<List<ulong>> { data.BreakEndTime.Select(bigInteger => (ulong)bigInteger).ToList() }
            };

            return StatusCode(200, new ResultViewModel<EmployeeDateViewModel>(result));
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

            if (!await IsValidTimestamp(timestamp))
                return NotFound(new ResultViewModel<string>("Invalid timestamp"));

            var service = new PontoBlockReportsService(_web3, PontoBlockReports.AddressContract);

            var date = await ValidateDateAsync(timestamp);
            if (date == null)
                return NotFound(new ResultViewModel<string>("Invalid timestamp"));

            var data = await service.GetWorkTimesForAllEmployeesAtDateQueryAsync((ulong)date);

            var result = new EmployeeDateViewModel
            {
                Date = new List<ulong> { (ulong)date },
                Address = new List<List<string>> {  data.EmpAddress  },
                StartWork = new List<List<ulong>> { data.StartWork.Select(bigInteger => (ulong)bigInteger).ToList() },
                EndWork = new List<List<ulong>> { data.EndWork.Select(bigInteger => (ulong)bigInteger).ToList() },
                BreakStartTime = new List<List<ulong>> { data.BreakStartTime.Select(bigInteger => (ulong)bigInteger).ToList() },
                BreakEndTime = new List<List<ulong>> { data.BreakEndTime.Select(bigInteger => (ulong)bigInteger).ToList() }
            };

            return StatusCode(200, new ResultViewModel<EmployeeDateViewModel>(result));
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

            if (!await IsValidTimestamp(startTimestamp))
                return NotFound(new ResultViewModel<string>("Invalid startTimestamp"));

            if (!await IsValidTimestamp(endTimestamp))
                return NotFound(new ResultViewModel<string>("Invalid endTimestamp"));

            var service = new PontoBlockReportsService(_web3, PontoBlockReports.AddressContract);

            var startDate = await ValidateDateAsync(startTimestamp);
            if (startDate == null)
                return NotFound(new ResultViewModel<string>("Invalid startTimestamp"));

            var endDate = await ValidateDateAsync(endTimestamp);
            if (endDate == null)
                return NotFound(new ResultViewModel<string>("Invalid endTimestamp"));

            var data = await service.GetWorkTimesForAllEmployeesBetweenTwoDatesQueryAsync((ulong)startDate, (ulong) endDate);

            var result = new EmployeeDateViewModel
            {
                Date = data.Date.Select(bigInteger => (ulong)bigInteger).ToList(),
                Address = data.EmpAddress,
                StartWork = data.StartWork.Select(bigInteger => bigInteger.Select(bigInteger1 => (ulong)bigInteger1).ToList()).ToList(),
                EndWork = data.EndWork.Select(bigInteger => bigInteger.Select(bigInteger1 => (ulong)bigInteger1).ToList()).ToList(),
                BreakStartTime = data.BreakEndTime.Select(bigInteger => bigInteger.Select(bigInteger1 => (ulong)bigInteger1).ToList()).ToList(),
                BreakEndTime = data.BreakEndTime.Select(bigInteger => bigInteger.Select(bigInteger1 => (ulong)bigInteger1).ToList()).ToList()
            };

            return StatusCode(200, new ResultViewModel<EmployeeDateViewModel>(result));
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
            var utilContract = GetContractsInMemory()!.Result.Find(x => x.Name == "UtilContract");
            if (utilContract == null)
                return null;

            var utilService = new UtilContractService(_web3, utilContract.AddressContract);

            var result = await utilService.GetDateQueryAsync(timestamp);

            return (ulong)result;
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message.ToString());
            return null;
        }
        
    }

    private async Task<ulong?> GetCreationDateContractAsync()
    {
        try
        {
            var pontoBlock = GetContractsInMemory()!.Result.Find(x => x.Name == "PontoBlock");
            if (pontoBlock == null)
                return null;

            var pontoBlockService = new PontoBlockService(_web3, pontoBlock.AddressContract);

            var result = await pontoBlockService.GetCreationDateContractQueryAsync();

            return (ulong)result;
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message.ToString());
            return null;
        }
    }

    private async Task<bool> IsValidTimestamp(int timestamp)
    {
        if (timestamp <= 0)
            return false;
        var date = await ValidateDateAsync(timestamp);
        var creationDate = await GetCreationDateContractAsync();

        if (date == null || creationDate == null) return false;

        if (date < creationDate) return false;

        return true;
    }
}