using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using WorkBlockApi.Data;
using WorkBlockApi.Extensions;
using WorkBlockApi.Interfaces;
using WorkBlockApi.Models;
using WorkBlockApi.Models.Employer;
using WorkBlockApi.Models.Employer.Events;
using WorkBlockApi.SmartContractsDefinitions.EmployerContract;
using WorkBlockApi.ViewModels;

namespace WorkBlockApi.Controllers.Contracts;

[ApiController]
[Route("v1/contracts/employercontract")]
public class EmployerContractController : ControllerBase
{
    private readonly Web3 _web3;
    private readonly IContractModelRepository _contractModelRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly ApiConfiguration _configuration;
    private const string ContractName = "EmployerContract";

    private ContractModel? EmployerContract { get; }

    public EmployerContractController(
        IContractModelRepository contractModelRepository,
        ApiConfiguration configuration,
        IMemoryCache memoryCache)
    {
        _configuration = configuration;
        _web3 = new Web3(new Account(configuration.PrivateKey), configuration.Provider);
        _contractModelRepository = contractModelRepository;
        _memoryCache = memoryCache;
        EmployerContract = GetContractsInMemory()!.Result.FirstOrDefault(x => x.Name == ContractName);
    }

    [HttpGet("Get/{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        try
        {
            if (EmployerContract is null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            if (id < 0)
                return NotFound(new ResultViewModel<string>("Invalid Format. Id must be equal to or greater than zero"));


            var service = new EmployerContractService(_web3, EmployerContract.AddressContract);
            var employer = await service.GetEmployerByIdQueryAsync(id);
            var returnEmployer = new EmployerModel
            {

                IdEmployer = (uint)employer.ReturnValue1.IdEmployer,
                Address = employer.ReturnValue1.EmployerAddress,
                Name = employer.ReturnValue1.Name,
                TaxId = employer.ReturnValue1.TaxId.ToString(),
                LegalAddress = employer.ReturnValue1.LegalAddress
            };
            return StatusCode(200, new ResultViewModel<EmployerModel>(returnEmployer));
        }
        catch (SmartContractRevertException e)
        {
            return StatusCode(500, new ResultViewModel<EmployerModel>(e.RevertMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<EmployerModel>(e.Message));
        }
    }

    [HttpGet("Get/{address}")]
    public async Task<IActionResult> GetByAddress([FromRoute] string address)
    {
        try
        {
            if (EmployerContract is null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));
            
            var service = new EmployerContractService(_web3, EmployerContract.AddressContract);
            var employer = await service.GetEmployerByAddressQueryAsync(address);
            var returnEmployer = new EmployerModel
            {

                IdEmployer = (uint)employer.ReturnValue1.IdEmployer,
                Address = employer.ReturnValue1.EmployerAddress,
                Name = employer.ReturnValue1.Name,
                TaxId = employer.ReturnValue1.TaxId.ToString(),
                LegalAddress = employer.ReturnValue1.LegalAddress
            };
            return StatusCode(200, new ResultViewModel<EmployerModel>(returnEmployer));
        }
        catch (SmartContractRevertException e)
        {
            return StatusCode(500, new ResultViewModel<EmployerModel>(e.RevertMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<EmployerModel>(e.Message));
        }
    }

    [HttpGet("GetAll/")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            if (EmployerContract is null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            var service = new EmployerContractService(_web3, EmployerContract.AddressContract);
            var employers = await service.GetAllEmployersQueryAsync();
            IList<EmployerModel> list = employers
                    .ReturnValue1
                    .Select(employer => new EmployerModel
                    {
                        IdEmployer = (uint)employer.IdEmployer,
                        Address = employer.EmployerAddress,
                        Name = employer.Name,
                        TaxId = employer.TaxId.ToString(),
                        LegalAddress = employer.LegalAddress
                    })
                    .ToList();

            return StatusCode(200, new ResultViewModel<IList<EmployerModel>>(list));
        }
        catch (SmartContractRevertException e)
        {
            return StatusCode(500, new ResultViewModel<IList<EmployerModel>>(e.RevertMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<IList<EmployerModel>>(e.Message));
        }
    }

    [HttpPost("Add")]
    public async Task<IActionResult> AddEmployer([FromBody] EmployerViewModel model, [FromServices] WorkBlockContext context)
    {
        try
        {
            if (EmployerContract is null)
                return NotFound(new ResultViewModel<ulong>("Contract Not Found"));
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var service = new EmployerContractService(_web3, EmployerContract.AddressContract);

            var result = await service.AddEmployerRequestAsync(model.Address,model.TaxId, model.Name, model.LegalAddress.ToString());
            
            var resultAdd = new EmployerAddedEventModel
            {
                AddressFrom = _configuration.AdminAddress,
                EmployerAddress = $"0x{model.Address}",
                EmployerName = model.Name.ToUpper(),
                EmployerTaxId = model.TaxId.ToString(),
                EmployerLegalAddress = model.LegalAddress.ToString(),
                Time = DateTime.UtcNow,
                HashTransaction = result
            };

            await context.EmployerAddedEvents.AddAsync(resultAdd);
            await context.SaveChangesAsync();

            return StatusCode(201, new ResultViewModel<EmployerAddedEventModel>(resultAdd));

        }
        catch (SmartContractRevertException e)
        {
            return StatusCode(500, new ResultViewModel<EmployerAddedEventModel>(e.RevertMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<EmployerAddedEventModel>(e.Message));
        }
    }

    [HttpPut("Update/{key}")]
    public async Task<IActionResult> UpdateEmployer([FromRoute] string key,
                                                    [FromBody] EmployerViewModel model,
                                                    [FromServices] WorkBlockContext context)
    {
        try
        {
            if (EmployerContract is null)
                return NotFound(new ResultViewModel<ulong>("Contract Not Found"));

            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            if (string.IsNullOrEmpty(key))
                return BadRequest(new ResultViewModel<string>("Key is a mandatory parameter"));

            var service = new EmployerContractService(_web3, EmployerContract.AddressContract);

            var result = await service.UpdateEmployerRequestAsync(key,model.Address, model.TaxId, model.Name, model.LegalAddress.ToString());

            var resultUpdate = new EmployerUpdatedEventModel
            {
                AddressFrom = _configuration.AdminAddress,
                OldAddress = $"0x{key}",
                NewAddress = $"0x{model.Address}",
                EmployerName = model.Name.ToUpper(),
                EmployerTaxId = model.TaxId.ToString(),
                EmployerLegalAddress = model.LegalAddress.ToString(),
                Time = DateTime.UtcNow,
                HashTransaction = result
            };

            await context.EmployerUpdatedEvents.AddAsync(resultUpdate);
            await context.SaveChangesAsync();

            return StatusCode(200, new ResultViewModel<EmployerUpdatedEventModel>(resultUpdate));
        }
        catch (SmartContractRevertException e)
        {
            return StatusCode(500, new ResultViewModel<EmployerUpdatedEventModel>(e.RevertMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<EmployerUpdatedEventModel>(e.Message));
        }
    }

    [HttpGet("Check/{address}")]
    public async Task<IActionResult> Check([FromRoute] string address)
    {
        try
        {
            if (EmployerContract is null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            var service = new EmployerContractService(_web3, EmployerContract.AddressContract);
            var result = await service.CheckIfEmployerExistsQueryAsync(address);

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