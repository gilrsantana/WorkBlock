using System.Numerics;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.ContractHandlers;
using Contracts.Contracts.UtilContract.ContractDefinition;

namespace WorkBlockApi.SmartContractsDefinitions.UtilContract;

public partial class UtilContractService
{
    public static Task<TransactionReceipt> 
        DeployContractAndWaitForReceiptAsync(
            Nethereum.Web3.Web3 web3, 
            UtilContractDeployment utilContractDeployment, 
            CancellationTokenSource? cancellationTokenSource = null)
    {
        return web3.Eth.GetContractDeploymentHandler<UtilContractDeployment>().SendRequestAndWaitForReceiptAsync(utilContractDeployment, cancellationTokenSource);
    }

    public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, UtilContractDeployment utilContractDeployment)
    {
        return web3.Eth.GetContractDeploymentHandler<UtilContractDeployment>().SendRequestAsync(utilContractDeployment);
    }

    public static async Task<UtilContractService> 
        DeployContractAndGetServiceAsync(
            Nethereum.Web3.Web3 web3, 
            UtilContractDeployment utilContractDeployment, 
            CancellationTokenSource? cancellationTokenSource = null)
    {
        var receipt = await DeployContractAndWaitForReceiptAsync(web3, utilContractDeployment, cancellationTokenSource);
        return new UtilContractService(web3, receipt.ContractAddress);
    }

    protected Nethereum.Web3.IWeb3 Web3{ get; }

    public ContractHandler ContractHandler { get; }

    public UtilContractService(Nethereum.Web3.Web3 web3, string contractAddress)
    {
        Web3 = web3;
        ContractHandler = web3.Eth.GetContractHandler(contractAddress);
    }

    public UtilContractService(Nethereum.Web3.IWeb3 web3, string contractAddress)
    {
        Web3 = web3;
        ContractHandler = web3.Eth.GetContractHandler(contractAddress);
    }

    public Task<BigInteger> GetDateQueryAsync(GetDateFunction getDateFunction, BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryAsync<GetDateFunction, BigInteger>(getDateFunction, blockParameter);
    }

        
    public Task<BigInteger> GetDateQueryAsync(BigInteger timestamp, BlockParameter? blockParameter = null)
    {
        var getDateFunction = new GetDateFunction();
        getDateFunction.Timestamp = timestamp;
            
        return ContractHandler.QueryAsync<GetDateFunction, BigInteger>(getDateFunction, blockParameter);
    }

    public Task<bool> ValidateTimeQueryAsync(ValidateTimeFunction validateTimeFunction, BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryAsync<ValidateTimeFunction, bool>(validateTimeFunction, blockParameter);
    }

        
    public Task<bool> ValidateTimeQueryAsync(BigInteger time, BlockParameter? blockParameter = null)
    {
        var validateTimeFunction = new ValidateTimeFunction();
        validateTimeFunction.Time = time;
            
        return ContractHandler.QueryAsync<ValidateTimeFunction, bool>(validateTimeFunction, blockParameter);
    }
}