using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using WorkBlockApi.Data;
using WorkBlockApi.Extensions;
using WorkBlockApi.Interfaces;
using WorkBlockApi.Models;
using WorkBlockApi.Models.Employee;
using WorkBlockApi.Models.Employee.Events;
using WorkBlockApi.SmartContractsDefinitions.EmployeeContract;
using WorkBlockApi.SmartContractsDefinitions.UtilContract;
using WorkBlockApi.ViewModels;

namespace WorkBlockApi.Controllers.Contracts;

[ApiController]
[Route("v1/contracts/employeecontract")]
public class EmployeeContractController : ControllerBase
{
    private readonly Web3 _web3;
    private readonly IContractModelRepository _contractModelRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly ApiConfiguration _configuration;
    private const string ContractName = "EmployeeContract";

    private ContractModel? EmployeeContract { get; }

    public EmployeeContractController(
        IContractModelRepository contractModelRepository,
        ApiConfiguration configuration,
        IMemoryCache memoryCache)
    {
        _configuration = configuration;
        _web3 = new Web3(new Account(configuration.PrivateKey), configuration.Provider);
        _contractModelRepository = contractModelRepository;
        _memoryCache = memoryCache;
        EmployeeContract = GetContractsInMemory()!.Result.Find(x => x.Name == ContractName);
    }

    [HttpGet("Get/{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        try
        {
            if (EmployeeContract is null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            if (id < 0)
                return NotFound(new ResultViewModel<string>("Invalid Format. Id must be equal to or greater than zero"));


            var service = new EmployeeContractService(_web3, EmployeeContract.AddressContract);
            var employee = await service.GetEmployeeByIdQueryAsync(id);
            var returnEmployee = new EmployeeModel
            {
                IdEmployee = (uint)employee.ReturnValue1.IdEmployee,
                Address = employee.ReturnValue1.EmployeeAddress,
                Name = employee.ReturnValue1.Name,
                TaxId = employee.ReturnValue1.TaxId.ToString(),
                BegginingWorkDay = (uint)employee.ReturnValue1.BegginingWorkDay,
                EndWorkDay = (uint)employee.ReturnValue1.EndWorkDay,
                StateOf = employee.ReturnValue1.StateOf,
                EmployerAddress = employee.ReturnValue1.EmployerAddress
            };

            return StatusCode(200, new ResultViewModel<EmployeeModel>(returnEmployee));
        }
        catch (SmartContractRevertException e)
        {
            return StatusCode(500, new ResultViewModel<EmployeeModel>(e.RevertMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<EmployeeModel>(e.Message));
        }
    }

    [HttpGet("Get/{address}")]
    public async Task<IActionResult> GetByAddress([FromRoute] string address)
    {
        try
        {
            if (EmployeeContract is null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            var service = new EmployeeContractService(_web3, EmployeeContract.AddressContract);
            var employee = await service.GetEmployeeByAddressQueryAsync(address);
            var returnEmployee = new EmployeeModel
            {
                IdEmployee = (uint)employee.ReturnValue1.IdEmployee,
                Address = employee.ReturnValue1.EmployeeAddress,
                Name = employee.ReturnValue1.Name,
                TaxId = employee.ReturnValue1.TaxId.ToString(),
                BegginingWorkDay = (uint)employee.ReturnValue1.BegginingWorkDay,
                EndWorkDay = (uint)employee.ReturnValue1.EndWorkDay,
                StateOf = employee.ReturnValue1.StateOf,
                EmployerAddress = employee.ReturnValue1.EmployerAddress
            };
            return StatusCode(200, new ResultViewModel<EmployeeModel>(returnEmployee));
        }
        catch (SmartContractRevertException e)
        {
            return StatusCode(500, new ResultViewModel<EmployeeModel>(e.RevertMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<EmployeeModel>(e.Message));
        }
    }

    [HttpGet("GetAll/")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            if (EmployeeContract is null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            var service = new EmployeeContractService(_web3, EmployeeContract.AddressContract);
            var employees = await service.GetAllEmployeesQueryAsync();
            IList<EmployeeModel> list = employees
                    .ReturnValue1
                    .Select(employee => new EmployeeModel
                    {
                        IdEmployee = (uint)employee.IdEmployee,
                        Address = employee.EmployeeAddress,
                        Name = employee.Name,
                        TaxId = employee.TaxId.ToString(),
                        BegginingWorkDay = (uint)employee.BegginingWorkDay,
                        EndWorkDay = (uint)employee.EndWorkDay,
                        StateOf = employee.StateOf,
                        EmployerAddress = employee.EmployerAddress
                    })
                    .ToList();

            return StatusCode(200, new ResultViewModel<IList<EmployeeModel>>(list));
        }
        catch (SmartContractRevertException e)
        {
            return StatusCode(500, new ResultViewModel<IList<EmployeeModel>>(e.RevertMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<IList<EmployeeModel>>(e.Message));
        }
    }

    [HttpPost("Add")]
    public async Task<IActionResult> AddEmployee([FromBody] EmployeeViewModel model, [FromServices] WorkBlockContext context)
    {
        try
        {
            if (EmployeeContract is null)
                return NotFound(new ResultViewModel<ulong>("Contract Not Found"));
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var utilContract = GetContractsInMemory()!.Result.Find(x => x.Name == "UtilContract");
            if (utilContract == null)
                return NotFound(new ResultViewModel<ulong>("Contract Not Found"));

            var utilService = new UtilContractService(_web3, utilContract.AddressContract);

            if (!await utilService.ValidateTimeQueryAsync(model.BegginingWorkDay))
                return BadRequest(new ResultViewModel<string>("Invalid BegginingWorkday format"));

            if (!await utilService.ValidateTimeQueryAsync(model.EndWorkDay))
                return BadRequest(new ResultViewModel<string>("Invalid EndWorkDay format"));

            var service = new EmployeeContractService(_web3, EmployeeContract.AddressContract);

            var result = await service.AddEmployeeRequestAsync(model.Address, model.Name.ToUpper(), model.TaxId, model.BegginingWorkDay, model.EndWorkDay, model.EmployerAddress );

            var resultAdd = new EmployeeAddedEventModel
            {
                AddressFrom = _configuration.AdminAddress,
                EmployeeAddress = model.Address,
                EmployeeName = model.Name.ToUpper(),
                EmployeeTaxId = model.TaxId.ToString(),
                EmployeeBegginingWorkDay = ConvertWorkDayToTime(model.BegginingWorkDay),
                EmployeeEndWorkDay = ConvertWorkDayToTime(model.EndWorkDay),
                EmployerAddress = model.EmployerAddress,
                Time = DateTime.UtcNow,
                HashTransaction = result
            };

            await context.EmployeeAddedEvents.AddAsync(resultAdd);
            await context.SaveChangesAsync();

            return StatusCode(201, new ResultViewModel<EmployeeAddedEventModel>(resultAdd));

        }
        catch (SmartContractRevertException e)
        {
            return StatusCode(500, new ResultViewModel<EmployeeAddedEventModel>(e.RevertMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<EmployeeAddedEventModel>(e.Message));
        }
    }

    [HttpPut("Update/{key}")]
    public async Task<IActionResult> UpdateEmployer([FromRoute] string key,
                                                    [FromBody] EmployeeViewModel model,
                                                    [FromServices] WorkBlockContext context)
    {
        try
        {
            if (EmployeeContract is null)
                return NotFound(new ResultViewModel<ulong>("Contract Not Found"));

            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            if (string.IsNullOrEmpty(key))
                return BadRequest(new ResultViewModel<string>("Key is a mandatory parameter"));

            var utilContract = GetContractsInMemory()!.Result.Find(x => x.Name == "UtilContract");
            if (utilContract == null)
                return NotFound(new ResultViewModel<ulong>("Contract Not Found"));

            var utilService = new UtilContractService(_web3, utilContract.AddressContract);

            if (!await utilService.ValidateTimeQueryAsync(model.BegginingWorkDay))
                return BadRequest(new ResultViewModel<string>("Invalid BegginingWorkday format"));

            if (!await utilService.ValidateTimeQueryAsync(model.EndWorkDay))
                return BadRequest(new ResultViewModel<string>("Invalid EndWorkDay format"));

            var service = new EmployeeContractService(_web3, EmployeeContract.AddressContract);

            var result = await service.UpdateEmployeeRequestAsync(key, model.Address, model.TaxId, model.Name.ToUpper(), model.BegginingWorkDay, model.EndWorkDay, model.State, model.EmployerAddress);

            var resultUpdate = new EmployeeUpdatedEventModel
            {
                AddressFrom = _configuration.AdminAddress,
                OldAddress = key,
                NewAddress = model.Address,
                EmployeeName = model.Name,
                EmployeeTaxId = model.TaxId.ToString(),
                EmployeeBegginingWorkDay = ConvertWorkDayToTime(model.BegginingWorkDay),
                EmployeeEndWorkDay = ConvertWorkDayToTime(model.EndWorkDay),
                EmployerAddress = model.EmployerAddress,
                State = model.State,
                Time = DateTime.UtcNow,
                HashTransaction = result
            };

            await context.EmployeeUpdatedEvents.AddAsync(resultUpdate);
            await context.SaveChangesAsync();

            return StatusCode(200, new ResultViewModel<EmployeeUpdatedEventModel>(resultUpdate));
        }
        catch (SmartContractRevertException e)
        {
            return StatusCode(500, new ResultViewModel<EmployeeUpdatedEventModel>(e.RevertMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<EmployeeUpdatedEventModel>(e.Message));
        }
    }

    [HttpGet("Check/{address}")]
    public async Task<IActionResult> Check([FromRoute] string address)
    {
        try
        {
            if (EmployeeContract is null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            var service = new EmployeeContractService(_web3, EmployeeContract.AddressContract);
            var result = await service.CheckIfEmployeeExistsQueryAsync(address);

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

    [HttpGet("GetEmployerContract")]
    public async Task<IActionResult> GetEmployer()
    {
        try
        {
            if (EmployeeContract is null)
                return NotFound(new ResultViewModel<string>("Contract Not Found"));

            var service = new EmployeeContractService(_web3, EmployeeContract.AddressContract);
            var result = await service.GetEmployerContractQueryAsync();

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

    private static TimeOnly ConvertWorkDayToTime(uint value)
    {
        var time = value.ToString();
        var hour = int.Parse(time[..^2]);
        var minute = int.Parse(time[^2..]);
        return new TimeOnly(hour, minute);
    }
}