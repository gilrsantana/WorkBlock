using System.Numerics;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.ContractHandlers;
using WorkBlockApi.SmartContractsDefinitions.PontoBlock.ContractDefinition;

namespace WorkBlockApi.SmartContractsDefinitions.PontoBlock;

public partial class PontoBlockService
{
    public static Task<TransactionReceipt> 
        DeployContractAndWaitForReceiptAsync(
            Nethereum.Web3.Web3 web3, 
            PontoBlockDeployment pontoBlockDeployment, 
            CancellationTokenSource? cancellationTokenSource = null)
    {
        return web3.Eth.GetContractDeploymentHandler<PontoBlockDeployment>().SendRequestAndWaitForReceiptAsync(pontoBlockDeployment, cancellationTokenSource);
    }

    public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, PontoBlockDeployment pontoBlockDeployment)
    {
        return web3.Eth.GetContractDeploymentHandler<PontoBlockDeployment>().SendRequestAsync(pontoBlockDeployment);
    }

    public static async Task<PontoBlockService> 
        DeployContractAndGetServiceAsync(
            Nethereum.Web3.Web3 web3, 
            PontoBlockDeployment pontoBlockDeployment, 
            CancellationTokenSource? cancellationTokenSource = null)
    {
        var receipt = await DeployContractAndWaitForReceiptAsync(web3, pontoBlockDeployment, cancellationTokenSource);
        return new PontoBlockService(web3, receipt.ContractAddress);
    }

    protected Nethereum.Web3.IWeb3 Web3{ get; }

    public ContractHandler ContractHandler { get; }

    public PontoBlockService(Nethereum.Web3.Web3 web3, string contractAddress)
    {
        Web3 = web3;
        ContractHandler = web3.Eth.GetContractHandler(contractAddress);
    }

    public PontoBlockService(Nethereum.Web3.IWeb3 web3, string contractAddress)
    {
        Web3 = web3;
        ContractHandler = web3.Eth.GetContractHandler(contractAddress);
    }

    public Task<string> BreakEndTimeRequestAsync(BreakEndTimeFunction breakEndTimeFunction)
    {
        return ContractHandler.SendRequestAsync(breakEndTimeFunction);
    }

    public Task<string> BreakEndTimeRequestAsync()
    {
        return ContractHandler.SendRequestAsync<BreakEndTimeFunction>();
    }

    public Task<TransactionReceipt> 
        BreakEndTimeRequestAndWaitForReceiptAsync(
            BreakEndTimeFunction breakEndTimeFunction, 
            CancellationTokenSource? cancellationToken = null)
    {
        return ContractHandler.SendRequestAndWaitForReceiptAsync(breakEndTimeFunction, cancellationToken);
    }

    public Task<TransactionReceipt> BreakEndTimeRequestAndWaitForReceiptAsync(CancellationTokenSource? cancellationToken = null)
    {
        return ContractHandler.SendRequestAndWaitForReceiptAsync<BreakEndTimeFunction>(null!, cancellationToken);
    }

    public Task<string> BreakStartTimeRequestAsync(BreakStartTimeFunction breakStartTimeFunction)
    {
        return ContractHandler.SendRequestAsync(breakStartTimeFunction);
    }

    public Task<string> BreakStartTimeRequestAsync()
    {
        return ContractHandler.SendRequestAsync<BreakStartTimeFunction>();
    }

    public Task<TransactionReceipt> 
        BreakStartTimeRequestAndWaitForReceiptAsync(
            BreakStartTimeFunction breakStartTimeFunction, 
            CancellationTokenSource? cancellationToken = null)
    {
        return ContractHandler.SendRequestAndWaitForReceiptAsync(breakStartTimeFunction, cancellationToken);
    }

    public Task<TransactionReceipt> BreakStartTimeRequestAndWaitForReceiptAsync(CancellationTokenSource? cancellationToken = null)
    {
        return ContractHandler.SendRequestAndWaitForReceiptAsync<BreakStartTimeFunction>(null!, cancellationToken);
    }

    public Task<string> ChangeOwnerRequestAsync(ChangeOwnerFunction changeOwnerFunction)
    {
        return ContractHandler.SendRequestAsync(changeOwnerFunction);
    }

    public Task<TransactionReceipt> 
        ChangeOwnerRequestAndWaitForReceiptAsync(
            ChangeOwnerFunction changeOwnerFunction, 
            CancellationTokenSource? cancellationToken = null)
    {
        return ContractHandler.SendRequestAndWaitForReceiptAsync(changeOwnerFunction, cancellationToken);
    }

    public Task<string> ChangeOwnerRequestAsync(string newOwner)
    {
        var changeOwnerFunction = new ChangeOwnerFunction();
        changeOwnerFunction.NewOwner = newOwner;
            
        return ContractHandler.SendRequestAsync(changeOwnerFunction);
    }

    public Task<TransactionReceipt> 
        ChangeOwnerRequestAndWaitForReceiptAsync(
            string newOwner, 
            CancellationTokenSource? cancellationToken = null)
    {
        var changeOwnerFunction = new ChangeOwnerFunction();
        changeOwnerFunction.NewOwner = newOwner;
            
        return ContractHandler.SendRequestAndWaitForReceiptAsync(changeOwnerFunction, cancellationToken);
    }

    public Task<string> EndWorkRequestAsync(EndWorkFunction endWorkFunction)
    {
        return ContractHandler.SendRequestAsync(endWorkFunction);
    }

    public Task<string> EndWorkRequestAsync()
    {
        return ContractHandler.SendRequestAsync<EndWorkFunction>();
    }

    public Task<TransactionReceipt> 
        EndWorkRequestAndWaitForReceiptAsync(
            EndWorkFunction endWorkFunction, 
            CancellationTokenSource? cancellationToken = null)
    {
        return ContractHandler.SendRequestAndWaitForReceiptAsync(endWorkFunction, cancellationToken);
    }

    public Task<TransactionReceipt> EndWorkRequestAndWaitForReceiptAsync(CancellationTokenSource? cancellationToken = null)
    {
        return ContractHandler.SendRequestAndWaitForReceiptAsync<EndWorkFunction>(null!, cancellationToken);
    }

    public Task<BigInteger> 
        GetCreationDateContractQueryAsync(
            GetCreationDateContractFunction getCreationDateContractFunction, 
            BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryAsync<GetCreationDateContractFunction, BigInteger>(getCreationDateContractFunction, blockParameter);
    }

        
    public Task<BigInteger> GetCreationDateContractQueryAsync(BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryAsync<GetCreationDateContractFunction, BigInteger>(null!, blockParameter);
    }

    public Task<GetEmployeeRecordsOutputDTO> 
        GetEmployeeRecordsQueryAsync(
            GetEmployeeRecordsFunction getEmployeeRecordsFunction, 
            BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryDeserializingToObjectAsync<GetEmployeeRecordsFunction, GetEmployeeRecordsOutputDTO>(getEmployeeRecordsFunction, blockParameter);
    }

    public Task<GetEmployeeRecordsOutputDTO> 
        GetEmployeeRecordsQueryAsync(
            string address, 
            BigInteger date, 
            BlockParameter? blockParameter = null)
    {
        var getEmployeeRecordsFunction = new GetEmployeeRecordsFunction();
        getEmployeeRecordsFunction.Address = address;
        getEmployeeRecordsFunction.Date = date;
            
        return ContractHandler.QueryDeserializingToObjectAsync<GetEmployeeRecordsFunction, GetEmployeeRecordsOutputDTO>(getEmployeeRecordsFunction, blockParameter);
    }

    public Task<BigInteger> 
        GetMomentQueryAsync(
            GetMomentFunction getMomentFunction, 
            BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryAsync<GetMomentFunction, BigInteger>(getMomentFunction, blockParameter);
    }

        
    public Task<BigInteger> GetMomentQueryAsync(BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryAsync<GetMomentFunction, BigInteger>(null!, blockParameter);
    }

    public Task<string> 
        GetOwnerQueryAsync(
            GetOwnerFunction getOwnerFunction, 
            BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryAsync<GetOwnerFunction, string>(getOwnerFunction, blockParameter);
    }

        
    public Task<string> GetOwnerQueryAsync(BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryAsync<GetOwnerFunction, string>(null!, blockParameter);
    }

    public Task<BigInteger> 
        GetTimeZoneQueryAsync(
            GetTimeZoneFunction getTimeZoneFunction, 
            BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryAsync<GetTimeZoneFunction, BigInteger>(getTimeZoneFunction, blockParameter);
    }

        
    public Task<BigInteger> GetTimeZoneQueryAsync(BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryAsync<GetTimeZoneFunction, BigInteger>(null!, blockParameter);
    }

    public Task<string> StartWorkRequestAsync(StartWorkFunction startWorkFunction)
    {
        return ContractHandler.SendRequestAsync(startWorkFunction);
    }

    public Task<string> StartWorkRequestAsync()
    {
        return ContractHandler.SendRequestAsync<StartWorkFunction>();
    }

    public Task<TransactionReceipt> 
        StartWorkRequestAndWaitForReceiptAsync(
            StartWorkFunction startWorkFunction, 
            CancellationTokenSource? cancellationToken = null)
    {
        return ContractHandler.SendRequestAndWaitForReceiptAsync(startWorkFunction, cancellationToken);
    }

    public Task<TransactionReceipt> StartWorkRequestAndWaitForReceiptAsync(CancellationTokenSource? cancellationToken = null)
    {
        return ContractHandler.SendRequestAndWaitForReceiptAsync<StartWorkFunction>(null!, cancellationToken);
    }
}