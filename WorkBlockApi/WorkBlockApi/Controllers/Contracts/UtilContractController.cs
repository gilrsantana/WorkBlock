using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using WorkBlockApi.Interfaces;
using WorkBlockApi.Model;

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
    public async Task<IActionResult> GetDateAsync(int timestamp)
    {
        try
        {
            if (UtilContract == null)
                return NotFound();

            var contract = _web3.Eth.GetContract(UtilContract.Abi, UtilContract.AddressContract);
            var getDate = contract.GetFunction("getDate");

            if (timestamp == 0)
                timestamp = (int)DateTimeOffset.Now.ToUnixTimeSeconds();

            var returnDate = await getDate.CallAsync<uint>(timestamp);

            return Ok(returnDate);
        }
        catch (Exception)
        {
            return BadRequest("Internal Server error");
        }
    }

    [HttpGet("validatehour")]
    public async Task<IActionResult> ValidateTime(int hour)
    {
        try
        {
            if (hour < 0)
                return BadRequest("Hour must be equals or grater than zero");

            if (UtilContract == null)
                return NotFound();

            var contract = _web3.Eth.GetContract(UtilContract.Abi, UtilContract.AddressContract);
            var getDate = contract.GetFunction("validateTime");
            var isValid = await getDate.CallAsync<bool>(hour);

            return Ok(isValid);
        }
        catch (Exception)
        {
            return BadRequest("Internal Server error");
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