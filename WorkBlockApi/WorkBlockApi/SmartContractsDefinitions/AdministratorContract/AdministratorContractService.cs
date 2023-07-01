using System.Numerics;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.ContractHandlers;
using WorkBlockApi.SmartContractsDefinitions.AdministratorContract.ContractDefinition;

namespace WorkBlockApi.SmartContractsDefinitions.AdministratorContract;

public partial class AdministratorContractService
{
    public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(
        Nethereum.Web3.Web3 web3, 
        AdministratorContractDeployment administratorContractDeployment, 
        CancellationTokenSource? cancellationTokenSource = null)
    {
        return web3.Eth.GetContractDeploymentHandler<AdministratorContractDeployment>()
            .SendRequestAndWaitForReceiptAsync(administratorContractDeployment, cancellationTokenSource);
    }
    public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, AdministratorContractDeployment administratorContractDeployment)
    {
        return web3.Eth.GetContractDeploymentHandler<AdministratorContractDeployment>().SendRequestAsync(administratorContractDeployment);
    }
    public static async Task<AdministratorContractService> DeployContractAndGetServiceAsync(
        Nethereum.Web3.Web3 web3, 
        AdministratorContractDeployment administratorContractDeployment, 
        CancellationTokenSource? cancellationTokenSource = null)
    {
        var receipt = await DeployContractAndWaitForReceiptAsync(web3, administratorContractDeployment, cancellationTokenSource);
        return new AdministratorContractService(web3, receipt.ContractAddress);
    }
    protected Nethereum.Web3.IWeb3 Web3{ get; }
    public ContractHandler ContractHandler { get; }
    public AdministratorContractService(Nethereum.Web3.Web3 web3, string contractAddress)
    {
        Web3 = web3;
        ContractHandler = web3.Eth.GetContractHandler(contractAddress);
    }
    public AdministratorContractService(Nethereum.Web3.IWeb3 web3, string contractAddress)
    {
        Web3 = web3;
        ContractHandler = web3.Eth.GetContractHandler(contractAddress);
    }
    public Task<string> AddAdministratorRequestAsync(AddAdministratorFunction addAdministratorFunction)
    {
         return ContractHandler.SendRequestAsync(addAdministratorFunction);
    }
    public Task<string> AddAdministratorRequestAsync(string address, string name, BigInteger taxId)
    {
        var addAdministratorFunction = new AddAdministratorFunction();
            addAdministratorFunction.Address = address;
            addAdministratorFunction.Name = name;
            addAdministratorFunction.TaxId = taxId;
        
         return ContractHandler.SendRequestAsync(addAdministratorFunction);
    }
    public Task<TransactionReceipt> AddAdministratorRequestAndWaitForReceiptAsync(
        AddAdministratorFunction addAdministratorFunction, 
        CancellationTokenSource? cancellationToken = null)
    {
         return ContractHandler.SendRequestAndWaitForReceiptAsync(addAdministratorFunction, cancellationToken);
    }
    
    public Task<TransactionReceipt> AddAdministratorRequestAndWaitForReceiptAsync(
        string address, 
        string name, 
        BigInteger taxId, 
        CancellationTokenSource? cancellationToken = null)
    {
        var addAdministratorFunction = new AddAdministratorFunction();
            addAdministratorFunction.Address = address;
            addAdministratorFunction.Name = name;
            addAdministratorFunction.TaxId = taxId;
        
         return ContractHandler.SendRequestAndWaitForReceiptAsync(addAdministratorFunction, cancellationToken);
    }
    public Task<bool> CheckIfAdministratorExistsQueryAsync(
        CheckIfAdministratorExistsFunction checkIfAdministratorExistsFunction, 
        BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryAsync<CheckIfAdministratorExistsFunction, bool>(checkIfAdministratorExistsFunction, blockParameter);
    }
    
    public Task<bool> CheckIfAdministratorExistsQueryAsync(string address, BlockParameter? blockParameter = null)
    {
        var checkIfAdministratorExistsFunction = new CheckIfAdministratorExistsFunction();
            checkIfAdministratorExistsFunction.Address = address;
        
        return ContractHandler.QueryAsync<CheckIfAdministratorExistsFunction, bool>(checkIfAdministratorExistsFunction, blockParameter);
    }
    public Task<bool> CheckIfAdministratorIsActiveQueryAsync(
        CheckIfAdministratorIsActiveFunction checkIfAdministratorIsActiveFunction, 
        BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryAsync<CheckIfAdministratorIsActiveFunction, bool>(checkIfAdministratorIsActiveFunction, blockParameter);
    }
    
    public Task<bool> CheckIfAdministratorIsActiveQueryAsync(string address, BlockParameter? blockParameter = null)
    {
        var checkIfAdministratorIsActiveFunction = new CheckIfAdministratorIsActiveFunction();
            checkIfAdministratorIsActiveFunction.Address = address;
        
        return ContractHandler.QueryAsync<CheckIfAdministratorIsActiveFunction, bool>(checkIfAdministratorIsActiveFunction, blockParameter);
    }
    public Task<GetAdministratorOutputDTO> GetAdministratorQueryAsync(
        GetAdministratorFunction getAdministratorFunction, 
        BlockParameter? blockParameter = null)
    {
        return ContractHandler
            .QueryDeserializingToObjectAsync<GetAdministratorFunction, GetAdministratorOutputDTO>(getAdministratorFunction, blockParameter);
    }
    public Task<GetAdministratorOutputDTO> GetAdministratorQueryAsync(BigInteger id, BlockParameter? blockParameter = null)
    {
        var getAdministratorFunction = new GetAdministratorFunction();
            getAdministratorFunction.Id = id;
        
        return ContractHandler
            .QueryDeserializingToObjectAsync<GetAdministratorFunction, GetAdministratorOutputDTO>(getAdministratorFunction, blockParameter);
    }
    public Task<GetAllAdministratorsOutputDTO> GetAllAdministratorsQueryAsync(
        GetAllAdministratorsFunction getAllAdministratorsFunction, 
        BlockParameter? blockParameter = null)
    {
        return ContractHandler
            .QueryDeserializingToObjectAsync<GetAllAdministratorsFunction, GetAllAdministratorsOutputDTO>(
                getAllAdministratorsFunction, blockParameter);
    }
    public Task<GetAllAdministratorsOutputDTO> GetAllAdministratorsQueryAsync(BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryDeserializingToObjectAsync<GetAllAdministratorsFunction, GetAllAdministratorsOutputDTO>(null!, blockParameter);
    }
    public Task<string> UpdateAdministratorRequestAsync(UpdateAdministratorFunction updateAdministratorFunction)
    {
         return ContractHandler.SendRequestAsync(updateAdministratorFunction);
    }
    public Task<string> UpdateAdministratorRequestAsync(string addressKey, string address, BigInteger taxId, string name, byte state)
    {
        var updateAdministratorFunction = new UpdateAdministratorFunction();
            updateAdministratorFunction.AddressKey = addressKey;
            updateAdministratorFunction.Address = address;
            updateAdministratorFunction.TaxId = taxId;
            updateAdministratorFunction.Name = name;
            updateAdministratorFunction.State = state;
        
         return ContractHandler.SendRequestAsync(updateAdministratorFunction);
    }
    public Task<TransactionReceipt> UpdateAdministratorRequestAndWaitForReceiptAsync(
        UpdateAdministratorFunction updateAdministratorFunction, 
        CancellationTokenSource? cancellationToken = null)
    {
         return ContractHandler.SendRequestAndWaitForReceiptAsync(updateAdministratorFunction, cancellationToken);
    }
    public Task<TransactionReceipt> UpdateAdministratorRequestAndWaitForReceiptAsync(
        string addressKey, 
        string address, 
        BigInteger taxId, 
        string name, 
        byte state, 
        CancellationTokenSource? cancellationToken = null)
    {
        var updateAdministratorFunction = new UpdateAdministratorFunction();
            updateAdministratorFunction.AddressKey = addressKey;
            updateAdministratorFunction.Address = address;
            updateAdministratorFunction.TaxId = taxId;
            updateAdministratorFunction.Name = name;
            updateAdministratorFunction.State = state;
        
         return ContractHandler.SendRequestAndWaitForReceiptAsync(updateAdministratorFunction, cancellationToken);
    }
    
    
}