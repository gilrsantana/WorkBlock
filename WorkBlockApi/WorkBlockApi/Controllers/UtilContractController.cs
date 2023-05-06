using Microsoft.AspNetCore.Mvc;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using WorkBlockApi.Interfaces;

namespace WorkBlockApi.Controllers;

[ApiController]
[Route("v1/contracts/utilcontract")]
public class UtilContractController : ControllerBase
{
    private readonly Web3 _web3;
    private readonly IContractModelRepository _contractModelService;
    private const string ContractName = "UtilContract";

    public UtilContractController(IContractModelRepository contractModelService, ApiConfiguration configuration)
    {
        _web3 = new Web3(new Account(configuration.PrivateKey), configuration.Provider);
        _contractModelService = contractModelService;
    }


    [HttpGet("getdate")]
    public async Task<IActionResult> GetDateAsync( int timestamp)
    {
        var utilContract = await _contractModelService.GetByNameAsync(ContractName); 
        
        if (utilContract == null)
        {
            return NotFound();
        }

        var contract = _web3.Eth.GetContract(utilContract.Abi, utilContract.AddressContract);

        var getDate = contract.GetFunction("getDate");

        if (timestamp == 0) 
        { 
            timestamp = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
        }

        var mydate = await getDate.CallAsync<uint>(timestamp);

        return Ok(mydate);
    }

    [HttpGet("validatehour")]
    public async Task<IActionResult> ValidateTime(int hour)
    {
        if (hour < 0)
            return BadRequest("Hour must be equals or grater than zero");

        var utilContract = await _contractModelService.GetByNameAsync(ContractName);

        if (utilContract == null)
        {
            return NotFound();
        }

        var contract = _web3.Eth.GetContract(utilContract.Abi, utilContract.AddressContract);

        var getDate = contract.GetFunction("validateTime");
        
        var isValid = await getDate.CallAsync<bool>(hour);

        return Ok(isValid);
    }
}