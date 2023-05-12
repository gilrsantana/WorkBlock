using System.Numerics;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.ContractHandlers;
using WorkBlockApi.SmartContractsDefinitions.EmployeeContract.ContractDefinition;

namespace WorkBlockApi.SmartContractsDefinitions.EmployeeContract;

public partial class EmployeeContractService
{
    public static Task<TransactionReceipt> 
        DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, 
            EmployeeContractDeployment employeeContractDeployment, 
            CancellationTokenSource? cancellationTokenSource = null)
    {
        return web3.Eth.GetContractDeploymentHandler<EmployeeContractDeployment>().SendRequestAndWaitForReceiptAsync(employeeContractDeployment, cancellationTokenSource);
    }

    public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, EmployeeContractDeployment employeeContractDeployment)
    {
        return web3.Eth.GetContractDeploymentHandler<EmployeeContractDeployment>().SendRequestAsync(employeeContractDeployment);
    }

    public static async Task<EmployeeContractService> 
        DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, 
            EmployeeContractDeployment employeeContractDeployment, 
            CancellationTokenSource? cancellationTokenSource = null)
    {
        var receipt = await DeployContractAndWaitForReceiptAsync(web3, employeeContractDeployment, cancellationTokenSource);
        return new EmployeeContractService(web3, receipt.ContractAddress);
    }

    protected Nethereum.Web3.IWeb3 Web3{ get; }

    public ContractHandler ContractHandler { get; }

    public EmployeeContractService(Nethereum.Web3.Web3 web3, string contractAddress)
    {
        Web3 = web3;
        ContractHandler = web3.Eth.GetContractHandler(contractAddress);
    }

    public EmployeeContractService(Nethereum.Web3.IWeb3 web3, string contractAddress)
    {
        Web3 = web3;
        ContractHandler = web3.Eth.GetContractHandler(contractAddress);
    }

    public Task<string> AddEmployeeRequestAsync(AddEmployeeFunction addEmployeeFunction)
    {
        return ContractHandler.SendRequestAsync(addEmployeeFunction);
    }

    public Task<TransactionReceipt> 
        AddEmployeeRequestAndWaitForReceiptAsync(AddEmployeeFunction addEmployeeFunction, 
            CancellationTokenSource? cancellationToken = null)
    {
        return ContractHandler.SendRequestAndWaitForReceiptAsync(addEmployeeFunction, cancellationToken);
    }

    public Task<string> AddEmployeeRequestAsync(string address, string name, BigInteger taxId, BigInteger begginingWorkDay, BigInteger endWorkDay, string employerAddress)
    {
        var addEmployeeFunction = new AddEmployeeFunction();
        addEmployeeFunction.Address = address;
        addEmployeeFunction.Name = name;
        addEmployeeFunction.TaxId = taxId;
        addEmployeeFunction.BegginingWorkDay = begginingWorkDay;
        addEmployeeFunction.EndWorkDay = endWorkDay;
        addEmployeeFunction.EmployerAddress = employerAddress;
            
        return ContractHandler.SendRequestAsync(addEmployeeFunction);
    }

    public Task<TransactionReceipt> 
        AddEmployeeRequestAndWaitForReceiptAsync(string address, 
            string name, 
            BigInteger taxId, 
            BigInteger begginingWorkDay, 
            BigInteger endWorkDay, 
            string employerAddress, 
            CancellationTokenSource? cancellationToken = null)
    {
        var addEmployeeFunction = new AddEmployeeFunction();
        addEmployeeFunction.Address = address;
        addEmployeeFunction.Name = name;
        addEmployeeFunction.TaxId = taxId;
        addEmployeeFunction.BegginingWorkDay = begginingWorkDay;
        addEmployeeFunction.EndWorkDay = endWorkDay;
        addEmployeeFunction.EmployerAddress = employerAddress;
            
        return ContractHandler.SendRequestAndWaitForReceiptAsync(addEmployeeFunction, cancellationToken);
    }

    public Task<bool> 
        CheckIfEmployeeExistsQueryAsync(CheckIfEmployeeExistsFunction checkIfEmployeeExistsFunction, 
            BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryAsync<CheckIfEmployeeExistsFunction, bool>(checkIfEmployeeExistsFunction, blockParameter);
    }

        
    public Task<bool> 
        CheckIfEmployeeExistsQueryAsync(string address, 
            BlockParameter? blockParameter = null)
    {
        var checkIfEmployeeExistsFunction = new CheckIfEmployeeExistsFunction();
        checkIfEmployeeExistsFunction.Address = address;
            
        return ContractHandler.QueryAsync<CheckIfEmployeeExistsFunction, bool>(checkIfEmployeeExistsFunction, blockParameter);
    }

    public Task<GetAllEmployeesOutputDTO> 
        GetAllEmployeesQueryAsync(GetAllEmployeesFunction getAllEmployeesFunction, 
            BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryDeserializingToObjectAsync<GetAllEmployeesFunction, GetAllEmployeesOutputDTO>(getAllEmployeesFunction, blockParameter);
    }

    public Task<GetAllEmployeesOutputDTO> GetAllEmployeesQueryAsync(BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryDeserializingToObjectAsync<GetAllEmployeesFunction, GetAllEmployeesOutputDTO>(null!, blockParameter);
    }

    public Task<GetEmployeeByAddressOutputDTO> 
        GetEmployeeByAddressQueryAsync(GetEmployeeByAddressFunction getEmployeeByAddressFunction, 
            BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryDeserializingToObjectAsync<GetEmployeeByAddressFunction, GetEmployeeByAddressOutputDTO>(getEmployeeByAddressFunction, blockParameter);
    }

    public Task<GetEmployeeByAddressOutputDTO> 
        GetEmployeeByAddressQueryAsync(string address, 
            BlockParameter? blockParameter = null)
    {
        var getEmployeeByAddressFunction = new GetEmployeeByAddressFunction();
        getEmployeeByAddressFunction.Address = address;
            
        return ContractHandler.QueryDeserializingToObjectAsync<GetEmployeeByAddressFunction, GetEmployeeByAddressOutputDTO>(getEmployeeByAddressFunction, blockParameter);
    }

    public Task<GetEmployeeByIdOutputDTO> 
        GetEmployeeByIdQueryAsync(GetEmployeeByIdFunction getEmployeeByIdFunction, 
            BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryDeserializingToObjectAsync<GetEmployeeByIdFunction, GetEmployeeByIdOutputDTO>(getEmployeeByIdFunction, blockParameter);
    }

    public Task<GetEmployeeByIdOutputDTO> 
        GetEmployeeByIdQueryAsync(BigInteger id, 
            BlockParameter? blockParameter = null)
    {
        var getEmployeeByIdFunction = new GetEmployeeByIdFunction();
        getEmployeeByIdFunction.Id = id;
            
        return ContractHandler.QueryDeserializingToObjectAsync<GetEmployeeByIdFunction, GetEmployeeByIdOutputDTO>(getEmployeeByIdFunction, blockParameter);
    }

    public Task<string> 
        GetEmployerContractQueryAsync(GetEmployerContractFunction getEmployerContractFunction, 
            BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryAsync<GetEmployerContractFunction, string>(getEmployerContractFunction, blockParameter);
    }

        
    public Task<string> GetEmployerContractQueryAsync(BlockParameter? blockParameter = null)
    {
        return ContractHandler.QueryAsync<GetEmployerContractFunction, string>(null!, blockParameter);
    }

    public Task<string> UpdateEmployeeRequestAsync(UpdateEmployeeFunction updateEmployeeFunction)
    {
        return ContractHandler.SendRequestAsync(updateEmployeeFunction);
    }

    public Task<TransactionReceipt> 
        UpdateEmployeeRequestAndWaitForReceiptAsync(UpdateEmployeeFunction updateEmployeeFunction, 
            CancellationTokenSource? cancellationToken = null)
    {
        return ContractHandler.SendRequestAndWaitForReceiptAsync(updateEmployeeFunction, cancellationToken);
    }

    public Task<string> 
        UpdateEmployeeRequestAsync(
            string addressKey, 
            string address, 
            BigInteger taxId, 
            string name, 
            BigInteger begginingWorkDay, 
            BigInteger endWorkDay, 
            byte state, 
            string employerAddress)
    {
        var updateEmployeeFunction = new UpdateEmployeeFunction();
        updateEmployeeFunction.AddressKey = addressKey;
        updateEmployeeFunction.Address = address;
        updateEmployeeFunction.TaxId = taxId;
        updateEmployeeFunction.Name = name;
        updateEmployeeFunction.BegginingWorkDay = begginingWorkDay;
        updateEmployeeFunction.EndWorkDay = endWorkDay;
        updateEmployeeFunction.State = state;
        updateEmployeeFunction.EmployerAddress = employerAddress;
            
        return ContractHandler.SendRequestAsync(updateEmployeeFunction);
    }

    public Task<TransactionReceipt> 
        UpdateEmployeeRequestAndWaitForReceiptAsync(
            string addressKey, 
            string address, 
            BigInteger taxId, 
            string name, 
            BigInteger begginingWorkDay, 
            BigInteger endWorkDay, 
            byte state, 
            string employerAddress, 
            CancellationTokenSource? cancellationToken = null)
    {
        var updateEmployeeFunction = new UpdateEmployeeFunction();
        updateEmployeeFunction.AddressKey = addressKey;
        updateEmployeeFunction.Address = address;
        updateEmployeeFunction.TaxId = taxId;
        updateEmployeeFunction.Name = name;
        updateEmployeeFunction.BegginingWorkDay = begginingWorkDay;
        updateEmployeeFunction.EndWorkDay = endWorkDay;
        updateEmployeeFunction.State = state;
        updateEmployeeFunction.EmployerAddress = employerAddress;
            
        return ContractHandler.SendRequestAndWaitForReceiptAsync(updateEmployeeFunction, cancellationToken);
    }
}