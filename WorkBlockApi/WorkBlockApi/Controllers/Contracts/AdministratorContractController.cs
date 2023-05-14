using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Contracts;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using WorkBlockApi.Data;
using WorkBlockApi.Extensions;
using WorkBlockApi.Interfaces;
using WorkBlockApi.Models;
using WorkBlockApi.Models.Administrator.Events;
using WorkBlockApi.SmartContractsDefinitions.AdministratorContract;
using WorkBlockApi.SmartContractsDefinitions.AdministratorContract.ContractDefinition;
using WorkBlockApi.ViewModels;
using WorkBlockApi.ViewModels.Events.Administrator;

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

    [HttpPost("AddAdministrator")]
    public async Task<IActionResult> AddAdministrator([FromBody]AdministratorViewModel model, [FromServices] WorkBlockContext context)
    {
        try
        {
            if (AdministratorContract is null)
                return NotFound(new ResultViewModel<ulong>("Contract Not Found"));
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
            var service = new AdministratorContractService(_web3, AdministratorContract.AddressContract);
            
            var result = await service.AddAdministratorRequestAsync(model.Address, model.Name.ToUpper(), model.TaxId);
            //var adminAddedEventHandler =
            //    _web3.Eth.GetEvent<AdminAddedEventDTOBase>(AdministratorContract.AddressContract);
            //var filter = adminAddedEventHandler.CreateFilterInput(null, new[] { model.Address });

            //var logs = await adminAddedEventHandler.GetAllChangesAsync(filter);

            //if (logs.Count <= 0)
            //    return NotFound(new ResultViewModel<AdminAddedEventViewModel>("Not found event from model address"));

            //var resultAdd = new AdminAddedEventModel()
            //{
            //    AddressFrom = logs[0].Event.From,
            //    AdministratorAddress = logs[0].Event.Address,
            //    AdministratorName = logs[0].Event.Name,
            //    AdministratorTaxId = ((ulong)logs[0].Event.Taxid).ToString(),
            //    Time = DateTimeOffset.FromUnixTimeSeconds((long)logs[0].Event.Timestamp).DateTime,
            //    HashTransaction = result
            //};

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
            return StatusCode(500, new ResultViewModel<AdminAddedEventViewModel>(e.Message));
        }
    }





    // [HttpPost("UpdateAdministrator")]
    // public async Task<IActionResult> UpdateAdministrator(string addressKey, string addressToUpdate, uint256 taxId, string name, int state, string fromAddress)
    // {
    //     var contract = _web3.Eth.GetContract(AdministratorContract.Abi, AdministratorContract.AddressContract);
    //     var function = contract.GetFunction("updateAdministrator");

    //     var transactionInput = function.CreateTransactionInput(from: fromAddress,
    //         gas: new HexBigInteger(500000),
    //         value: new HexBigInteger(BigInteger.Zero),
    //         functionInput: new
    //         {
    //             _addressKey = addressKey,
    //             _address = addressToUpdate,
    //             _name = name,
    //             _taxId = taxId,
    //             _state = state
    //         });

    //     var transactionHash = await _web3.Eth.TransactionManager.SendTransactionAsync(transactionInput);

    //     return Ok(transactionHash);
    // }

    //[HttpGet("GetAdministrator")]
    //public async Task<IActionResult> GetAdministrator(int id)
    //{
    //    try
    //    {
    //        if (id < 0) return NotFound("Id must be equals or grater than zero.");

    //        if (AdministratorContract is null) return BadRequest("Internal Server error.");
    //        var service = new AdministratorContractService(_web3, AdministratorContract.AddressContract);
    //        var admin = await service.GetAdministratorQueryAsync(id);
    //        ulong adminId = (ulong)admin.ReturnValue1.IdAdministrator;
    //        ulong adminTaxId = (ulong)admin.ReturnValue1.TaxId;
    //        return Ok(new
    //        {
    //            adminId,
    //            admin.ReturnValue1.Name,
    //            adminTaxId,
    //            admin.ReturnValue1.AdministratorAddress,
    //            admin.ReturnValue1.StateOf
    //        });
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest($"Internal Server error. {ex.Message.ToString()}");
    //    }
    //}

    //[HttpGet("GetAllAdministrators")]
    //public async Task<IActionResult> GetAllAdministrators()
    //{
    //    if (AdministratorContract is null) return BadRequest("Internal Server error.");
    //    var service = new AdministratorContractService(_web3, AdministratorContract.AddressContract);
    //    var administrators = await service.GetAllAdministratorsQueryAsync();


    //    return Ok(administrators);
    //}

    // [HttpGet("CheckIfAdministratorExists")]
    // public async Task<IActionResult> CheckIfAdministratorExists(string address, string fromAddress)
    // {
    //     var contract = _web3.Eth.GetContract(AdministratorContract.Abi, AdministratorContract.AddressContract);
    //     var function = contract.GetFunction("checkIfAdministratorExists");

    //     var result = await function.CallAsync<bool>(fromAddress, address);

    //     return Ok(result);
    // }

    // [HttpGet("CheckIfAdministratorIsActive")]
    // public async Task<IActionResult> CheckIfAdministratorIsActive(string address, string fromAddress)
    // {
    //     var contract = _web3.Eth.GetContract(AdministratorContract.Abi, AdministratorContract.AddressContract);
    //     var function = contract.GetFunction("checkIfAdministratorIsActive");

    //     var result = await function.CallAsync<bool>(fromAddress, address);

    //     return Ok(result);
    // }

    //[HttpGet("getbyid/{id:int}")]
    //public async Task<ActionResult<Administrator>> GetAdministratorByIdAsync(int id)
    //{
    //    try
    //    {
    //        if (id < 0)
    //            return BadRequest("id must be equals or grater than zero");

    //        if (AdministratorContract == null)
    //            return NotFound();


    //        var contract = _web3.Eth.GetContract(AdministratorContract.Abi, AdministratorContract.AddressContract);
    //        var getAdministratorFunction = contract.GetFunction("getAdministrator");

    //        var transactionInput = getAdministratorFunction.CreateCallInput(id);
    //        var callResult = await _web3.Eth.Transactions.Call.SendRequestAsync(transactionInput, _configuration.PrivateKey, _configuration.AdminAddress);

    //        var administrator = new Administrator();
    //        administrator.IdAdministrator = callResult.DecodeSimpleType<uint256>();
    //        administrator.AdministratorAddress = callResult.DecodeAddress();
    //        administrator.TaxId = callResult.DecodeSimpleType<uint256>();
    //        administrator.Name = callResult.Decode<string>();
    //        administrator.StateOf = callResult.DecodeEnumeration<State>();

    //        //var hexString = id.ToString("X32");
    //        //var num = uint.Parse(hexString);

    //        //var senderAddress = _configuration.AdminAddress;
    //        //var function = new GetAdministratorFunction()
    //        //{
    //        //    Id = id,
    //        //    FromAddress = senderAddress
    //        //};


    //        //var functionHandler = _web3.Eth.GetContractQueryHandler<GetAdministratorFunction>();
    //        //var ad = new Administrator();

    //        // //ad = await functionHandler.QueryAsync<Administrator>(AdministratorContract.AddressContract, function);
    //        // ad = await functionHandler
    //        //     .QueryDeserializingToObjectAsync<Administrator>(function, AdministratorContract.AddressContract);

    //        // var contract = _web3.Eth.GetContract(AdministratorContract.Abi, AdministratorContract.AddressContract);
    //        // var getAdministrator = contract.GetFunction("getAdministrator");
    //        // var gas = await getAdministrator.EstimateGasAsync(senderAddress, null, null, id);

    //        // var result = await getAdministrator
    //        //     .CallAsync<Administrator>(senderAddress, gas, null, id);

    //        return Ok(ad);
    //    }
    //    catch (Exception)
    //    {
    //        return BadRequest("Internal Server error");
    //    }
    //}

    //[HttpGet("getall")]
    //public async Task<IActionResult> GetAllAdministratorsAsync()
    //{
    //    try
    //    {
    //        if (AdministratorContract == null)
    //            return NotFound();

    //        var senderAddress = _configuration.AdminAddress;
    //        var contract = _web3.Eth.GetContract(AdministratorContract.Abi, AdministratorContract.AddressContract);
    //        var getAllAdministrators = contract.GetFunction("getAllAdministrators");
    //        var value = new BigInteger();
    //        var gas = await getAllAdministrators.EstimateGasAsync(senderAddress, null, null);

    //        var result = await getAllAdministrators.CallDeserializingToObjectAsync<Administrator>(senderAddress, null, null);
    //        return Ok(result);
    //    }
    //    catch (SmartContractRevertException e)
    //    {
    //        return BadRequest($"Internal Server error. {e}");
    //    }
    //    catch (Exception e)
    //    {
    //        return BadRequest($"Internal Server error. {e}");
    //    }
    //}

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