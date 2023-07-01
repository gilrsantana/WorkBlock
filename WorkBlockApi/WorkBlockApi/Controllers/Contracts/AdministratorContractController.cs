using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using WorkBlockApi.Data;
using WorkBlockApi.Extensions;
using WorkBlockApi.Interfaces;
using WorkBlockApi.Models;
using WorkBlockApi.Models.Administrator;
using WorkBlockApi.Models.Administrator.Events;
using WorkBlockApi.SmartContractsDefinitions.AdministratorContract;
using WorkBlockApi.SmartContractsDefinitions.AdministratorContract.ContractDefinition;
using WorkBlockApi.ViewModels;

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
        AdministratorContract = GetContractsInMemory()!.Result.Find(x => x.Name == ContractName);
    }

    [HttpGet("Get/{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        try
        {
            if (AdministratorContract is null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            if (id < 0)
                return NotFound(new ResultViewModel<string>("Invalid Format. Id must be equal to or greater than zero"));


            var service = new AdministratorContractService(_web3, AdministratorContract.AddressContract);
            var admin = await service.GetAdministratorQueryAsync(id);
            var returnAdmin = new AdministratorModel
            {
                IdAdministrator = (uint)admin.ReturnValue1.IdAdministrator,
                Address = admin.ReturnValue1.AdministratorAddress,
                Name = admin.ReturnValue1.Name,
                TaxId = admin.ReturnValue1.TaxId.ToString(),
                State = admin.ReturnValue1.StateOf
            };
            return StatusCode(200, new ResultViewModel<AdministratorModel>(returnAdmin));
        }
        catch (SmartContractRevertException e)
        {
            return StatusCode(500, new ResultViewModel<AdministratorModel>(e.RevertMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<AdministratorModel>(e.Message));
        }
    }

    [HttpGet("GetAll/")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            if (AdministratorContract is null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            var service = new AdministratorContractService(_web3, AdministratorContract.AddressContract);
            var administrators = await service.GetAllAdministratorsQueryAsync();
            IList<AdministratorModel> list = administrators
                .ReturnValue1
                .Select(administrator => new AdministratorModel
                {
                    IdAdministrator = (uint)administrator.IdAdministrator,
                    Address = administrator.AdministratorAddress,
                    Name = administrator.Name,
                    TaxId = administrator.TaxId.ToString(),
                    State = administrator.StateOf
                })
                .ToList();

            return StatusCode(200, new ResultViewModel<IList<AdministratorModel>>(list));
        }
        catch (SmartContractRevertException e)
        {
            return StatusCode(500, new ResultViewModel<IList<AdministratorModel>>(e.RevertMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<IList<AdministratorModel>>(e.Message));
        }
    }

    [HttpPost("Add")]
    public async Task<IActionResult> AddAdministrator([FromBody]AdministratorViewModel model, [FromServices] WorkBlockContext context)
    {
        try
        {
            if (AdministratorContract is null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
            var service = new AdministratorContractService(_web3, AdministratorContract.AddressContract);
            
            var result = await service.AddAdministratorRequestAsync(model.Address, model.Name.ToUpper(), model.TaxId);

            var resultAdd = new AdminAddedEventModel
            {
                AddressFrom = _configuration.AdminAddress,
                AdministratorAddress = $"0x{model.Address}",
                AdministratorName = model.Name.ToUpper(),
                AdministratorTaxId = model.TaxId.ToString(),
                Time = DateTime.UtcNow,
                HashTransaction = result
            };

            await context.AdminAddedEvents.AddAsync(resultAdd);
            await context.SaveChangesAsync();

            return StatusCode(201, new ResultViewModel<AdminAddedEventModel>(resultAdd));

        }
        catch (SmartContractRevertException e)
        {
            return StatusCode(500, new ResultViewModel<AdminAddedEventDTOBase>(e.RevertMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<AdminAddedEventModel>(e.Message));
        }
    }

    [HttpPut("Update/{key}")]
    public async Task<IActionResult> UpdateAdministrator([FromRoute] string key, 
                                                         [FromBody] AdministratorViewModel model, 
                                                         [FromServices] WorkBlockContext context)
    {
        try
        {
            if(AdministratorContract is null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));
            
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
            
            if (string.IsNullOrEmpty(key))
                return BadRequest(new ResultViewModel<string>("Key is a mandatory parameter"));

            var service = new AdministratorContractService(_web3, AdministratorContract.AddressContract);

            var result = await service.UpdateAdministratorRequestAsync(key, model.Address, model.TaxId, model.Name.ToUpper(), model.State);

            var resultUpdate = new AdminUpdatedEventModel
            {
                AddressFrom = _configuration.AdminAddress,
                OldAddress = $"0x{key}",
                NewAddress = $"0x{model.Address}",
                AdministratorName = model.Name.ToUpper(),
                AdministratorTaxId = model.TaxId.ToString(),
                State = model.State,
                Time = DateTime.UtcNow,
                HashTransaction = result
            };

            await context.AdminUpdatedEvents.AddAsync(resultUpdate);
            await context.SaveChangesAsync();

            return StatusCode(200, new ResultViewModel<AdminUpdatedEventModel>(resultUpdate));
        }
        catch (SmartContractRevertException e)
        {
            return StatusCode(500, new ResultViewModel<AdminUpdatedEventModel>(e.RevertMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<AdminUpdatedEventModel>(e.Message));
        }
    }

    [HttpGet("Check/{address}")]
    public async Task<IActionResult> Check([FromRoute] string address)
    {
        try
        {
            if (AdministratorContract is null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            var service = new AdministratorContractService(_web3, AdministratorContract.AddressContract);
            var result = await service.CheckIfAdministratorExistsQueryAsync(address);

            return StatusCode(200, new ResultViewModel<bool>(result));
        }
        catch (SmartContractRevertException e)
        {
            return StatusCode(500, new ResultViewModel<bool>(e.RevertMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<bool>(e.Message));
        }
    }

    [HttpGet("CheckActive/{address}")]
    public async Task<IActionResult> CheckActive([FromRoute]string address)
    {
        try
        {
            if (AdministratorContract is null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            var service = new AdministratorContractService(_web3, AdministratorContract.AddressContract);
            var result = await service.CheckIfAdministratorIsActiveQueryAsync(address);

            return StatusCode(200, new ResultViewModel<bool>(result));
        }
        catch (SmartContractRevertException e)
        {
            return StatusCode(500, new ResultViewModel<AdminUpdatedEventDTO>(e.RevertMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<AdminUpdatedEventModel>(e.Message));
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