using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using WorkBlockApi.Interfaces;
using WorkBlockApi.Models;
using WorkBlockApi.SmartContractsDefinitions.UtilContract;
using WorkBlockApi.ViewModels;

namespace WorkBlockApi.Controllers.Contracts;

[ApiController]
[Route("v1/contracts/utilcontract")]
public class UtilContractController : ControllerBase
{
    private readonly Web3 _web3;
    private readonly IContractModelRepository _contractModelRepository;
    private readonly IMemoryCache _memoryCache;
    private const string ContractName = "UtilContract";

    private ContractModel? UtilContract { get; }

    public UtilContractController(
        IContractModelRepository contractModelRepository,
        ApiConfiguration configuration,
        IMemoryCache memoryCache)
    {
        _web3 = new Web3(new Account(configuration.PrivateKey), configuration.Provider);
        _contractModelRepository = contractModelRepository;
        _memoryCache = memoryCache;
        UtilContract = GetContractsInMemory()!.Result.FirstOrDefault(x => x.Name == ContractName);
    }

    [HttpGet("getdate")]
    public async Task<IActionResult> GetDateAsync(ulong timestamp)
    {
        try
        {
            if (UtilContract == null) 
                return NotFound(new ResultViewModel<ulong>("Contract Not Found"));
            var service = new UtilContractService(_web3, UtilContract.AddressContract);

            if (timestamp == 0) 
                timestamp = (ulong)DateTimeOffset.Now.ToUnixTimeSeconds();
            var returnDate = await service.GetDateQueryAsync(timestamp);

            return Ok(new ResultViewModel<ulong>((ulong)returnDate));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<ulong>("UCX01 - Internal Server Error"));
        }
    }

    [HttpGet("validatehour")]
    public async Task<IActionResult> ValidateTime(int hour)
    {
        try
        {
            if (UtilContract == null)
                return NotFound(new ResultViewModel<bool>("Contract Not Found"));
            if (hour < 0)
                return NotFound(new ResultViewModel<bool>("Invalid Format. Hour must be equal to or greater than zero"));
            var service = new UtilContractService(_web3, UtilContract.AddressContract);
            var isValid = await service.ValidateTimeQueryAsync(hour);
            
            return Ok(new ResultViewModel<bool>(isValid));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<bool>("UCX02 - Internal Server Error"));
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
}