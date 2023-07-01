using System.Numerics;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.ContractHandlers;
using WorkBlockApi.SmartContractsDefinitions.EmployerContract.ContractDefinition;

namespace WorkBlockApi.SmartContractsDefinitions.EmployerContract;

public partial class EmployerContractService
{
    public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(
        Nethereum.Web3.Web3 web3, 
        EmployerContractDeployment employerContractDeployment, 
        CancellationTokenSource? cancellationTokenSource = null)
    {
        return web3.Eth.GetContractDeploymentHandler<EmployerContractDeployment>()
            .SendRequestAndWaitForReceiptAsync(employerContractDeployment, cancellationTokenSource);
    }
    public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, EmployerContractDeployment employerContractDeployment)
    {
        return web3.Eth.GetContractDeploymentHandler<EmployerContractDeployment>().SendRequestAsync(employerContractDeployment);
    }
    public static async Task<EmployerContractService> DeployContractAndGetServiceAsync(
        Nethereum.Web3.Web3 web3, 
        EmployerContractDeployment employerContractDeployment, 
        CancellationTokenSource? cancellationTokenSource = null)
    {
        var receipt = await DeployContractAndWaitForReceiptAsync(web3, employerContractDeployment, cancellationTokenSource);
        return new EmployerContractService(web3, receipt.ContractAddress);
    }
    protected Nethereum.Web3.IWeb3 Web3{ get; }
    public ContractHandler ContractHandler { get; }
    public EmployerContractService(Nethereum.Web3.Web3 web3, string contractAddress)
    {
        Web3 = web3;
        ContractHandler = web3.Eth.GetContractHandler(contractAddress);
    }
    public EmployerContractService(Nethereum.Web3.IWeb3 web3, string contractAddress)
    {
        Web3 = web3;
        ContractHandler = web3.Eth.GetContractHandler(contractAddress);
    }
    public Task<string> AddEmployerRequestAsync(AddEmployerFunction addEmployerFunction)
    {
         return ContractHandler.SendRequestAsync(addEmployerFunction);
    }
    public Task<string> AddEmployerRequestAsync(string address, BigInteger taxId, string name, string legalAddress)
    {
        var addEmployerFunction = new AddEmployerFunction();
            addEmployerFunction.Address = address;
            addEmployerFunction.TaxId = taxId;
            addEmployerFunction.Name = name;
            addEmployerFunction.LegalAddress = legalAddress;
        
         return ContractHandler.SendRequestAsync(addEmployerFunction);
    }
    public Task<TransactionReceipt> AddEmployerRequestAndWaitForReceiptAsync(
        AddEmployerFunction addEmployerFunction, 
        CancellationTokenSource? cancellationToken = null)
    {
         return ContractHandler.SendRequestAndWaitForReceiptAsync(addEmployerFunction, cancellationToken);
    }
    
    public Task<TransactionReceipt> AddEmployerRequestAndWaitForReceiptAsync(
        string address, 
        BigInteger taxId, 
        string name, 
        string legalAddress, 
        CancellationTokenSource? cancellationToken = null)
    {
        var addEmployerFunction = new AddEmployerFunction();
            addEmployerFunction.Address = address;
            addEmployerFunction.TaxId = taxId;
            addEmployerFunction.Name = name;
            addEmployerFunction.LegalAddress = legalAddress;
        
         return ContractHandler.SendRequestAndWaitForReceiptAsync(addEmployerFunction, cancellationToken);
    }
    public Task<bool> CheckIfEmployerExistsQueryAsync(
        CheckIfEmployerExistsFunction checkIfEmployerExistsFunction, 
        BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryAsync<CheckIfEmployerExistsFunction, bool>(checkIfEmployerExistsFunction, blockParameter);
    }
    
    public Task<bool> CheckIfEmployerExistsQueryAsync(string address, BlockParameter? blockParameter = null)
    {
        var checkIfEmployerExistsFunction = new CheckIfEmployerExistsFunction();
            checkIfEmployerExistsFunction.Address = address;
        
        return ContractHandler.QueryAsync<CheckIfEmployerExistsFunction, bool>(checkIfEmployerExistsFunction, blockParameter);
    }
    public Task<GetAllEmployersOutputDTO> GetAllEmployersQueryAsync(
        GetAllEmployersFunction getAllEmployersFunction, 
        BlockParameter? blockParameter = null)
    {
        return ContractHandler
            .QueryDeserializingToObjectAsync<GetAllEmployersFunction, GetAllEmployersOutputDTO>(getAllEmployersFunction, blockParameter);
    }
    public Task<GetAllEmployersOutputDTO> GetAllEmployersQueryAsync(BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryDeserializingToObjectAsync<GetAllEmployersFunction, GetAllEmployersOutputDTO>(null!, blockParameter);
    }
    public Task<GetEmployerByAddressOutputDTO> GetEmployerByAddressQueryAsync(
        GetEmployerByAddressFunction getEmployerByAddressFunction, 
        BlockParameter? blockParameter = null)
    {
        return ContractHandler
            .QueryDeserializingToObjectAsync<GetEmployerByAddressFunction, GetEmployerByAddressOutputDTO>(
                getEmployerByAddressFunction, 
                blockParameter);
    }
    public Task<GetEmployerByAddressOutputDTO> GetEmployerByAddressQueryAsync(string address, BlockParameter? blockParameter = null)
    {
        var getEmployerByAddressFunction = new GetEmployerByAddressFunction();
            getEmployerByAddressFunction.Address = address;
        
        return ContractHandler
            .QueryDeserializingToObjectAsync<GetEmployerByAddressFunction, GetEmployerByAddressOutputDTO>(getEmployerByAddressFunction, blockParameter);
    }
    public Task<GetEmployerByIdOutputDTO> GetEmployerByIdQueryAsync(GetEmployerByIdFunction getEmployerByIdFunction, BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryDeserializingToObjectAsync<GetEmployerByIdFunction, GetEmployerByIdOutputDTO>(getEmployerByIdFunction, blockParameter);
    }
    public Task<GetEmployerByIdOutputDTO> GetEmployerByIdQueryAsync(BigInteger id, BlockParameter? blockParameter = null)
    {
        var getEmployerByIdFunction = new GetEmployerByIdFunction();
            getEmployerByIdFunction.Id = id;
        
        return ContractHandler.QueryDeserializingToObjectAsync<GetEmployerByIdFunction, GetEmployerByIdOutputDTO>(getEmployerByIdFunction, blockParameter);
    }
    public Task<string> UpdateEmployerRequestAsync(UpdateEmployerFunction updateEmployerFunction)
    {
         return ContractHandler.SendRequestAsync(updateEmployerFunction);
    }
    public Task<string> UpdateEmployerRequestAsync(string addressKey, string address, BigInteger taxId, string name, string legalAddress)
    {
        var updateEmployerFunction = new UpdateEmployerFunction();
            updateEmployerFunction.AddressKey = addressKey;
            updateEmployerFunction.Address = address;
            updateEmployerFunction.TaxId = taxId;
            updateEmployerFunction.Name = name;
            updateEmployerFunction.LegalAddress = legalAddress;
        
         return ContractHandler.SendRequestAsync(updateEmployerFunction);
    }
    public Task<TransactionReceipt> UpdateEmployerRequestAndWaitForReceiptAsync(
        UpdateEmployerFunction updateEmployerFunction, 
        CancellationTokenSource? cancellationToken = null)
    {
         return ContractHandler.SendRequestAndWaitForReceiptAsync(updateEmployerFunction, cancellationToken);
    }
    
    public Task<TransactionReceipt> UpdateEmployerRequestAndWaitForReceiptAsync(
        string addressKey, 
        string address, 
        BigInteger taxId, 
        string name, 
        string legalAddress, 
        CancellationTokenSource? cancellationToken = null)
    {
        var updateEmployerFunction = new UpdateEmployerFunction();
            updateEmployerFunction.AddressKey = addressKey;
            updateEmployerFunction.Address = address;
            updateEmployerFunction.TaxId = taxId;
            updateEmployerFunction.Name = name;
            updateEmployerFunction.LegalAddress = legalAddress;
        
         return ContractHandler.SendRequestAndWaitForReceiptAsync(updateEmployerFunction, cancellationToken);
    }
}
