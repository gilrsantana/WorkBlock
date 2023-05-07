using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using WorkBlockApi.Interfaces;
using WorkBlockApi.Model;

namespace WorkBlockApi.Controllers.Contracts;

[ApiController]
[Route("v1/contracts/administratorcontract")]
public class AdministratorContractController : ControllerBase
{
    private readonly Web3 _web3;
    private readonly IContractModelRepository _contractModelRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly ApiConfiguration _configuration;
    private const string ContractName = "AdministratorContract";

    private ContractModel? AdministratorContract { get; }

    public AdministratorContractController(
        IContractModelRepository contractModelRepository,
        ApiConfiguration configuration,
        IMemoryCache memoryCache)
    {
        _configuration = configuration;
        _web3 = new Web3(new Account(configuration.PrivateKey), configuration.Provider);
        _contractModelRepository = contractModelRepository;
        _memoryCache = memoryCache;
        AdministratorContract = GetContractsInMemory()!.Result.FirstOrDefault(x => x.Name == ContractName);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAdministratorByIdAsync(int id)
    {
        try
        {
            if (id < 0)
                return BadRequest("id must be equals or grater than zero");

            if (AdministratorContract == null)
                return NotFound();

            var senderAddress = _configuration.AdminAddress;
            var contract = _web3.Eth.GetContract(AdministratorContract.Abi, AdministratorContract.AddressContract);
            var getAdministrator = contract.GetFunction("getAdministrator");
            var gas = await getAdministrator.EstimateGasAsync(senderAddress, null, null, id);
            var administrator = await getAdministrator.CallAsync<AdministratorModel>(senderAddress, gas, null, id);

            return Ok(administrator);
        }
        catch (Exception )
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