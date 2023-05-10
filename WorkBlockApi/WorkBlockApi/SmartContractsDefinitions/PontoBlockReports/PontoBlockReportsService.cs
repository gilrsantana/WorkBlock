using System.Numerics;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.ContractHandlers;
using WorkBlockApi.SmartContractsDefinitions.PontoBlockReports.ContractDefinition;

namespace WorkBlockApi.SmartContractsDefinitions.PontoBlockReports;

public partial class PontoBlockReportsService
{
    public static Task<TransactionReceipt> 
        DeployContractAndWaitForReceiptAsync(
            Nethereum.Web3.Web3 web3, 
            PontoBlockReportsDeployment pontoBlockReportsDeployment, 
            CancellationTokenSource? cancellationTokenSource = null)
    {
        return web3.Eth.GetContractDeploymentHandler<PontoBlockReportsDeployment>().SendRequestAndWaitForReceiptAsync(pontoBlockReportsDeployment, cancellationTokenSource);
    }

    public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, PontoBlockReportsDeployment pontoBlockReportsDeployment)
    {
        return web3.Eth.GetContractDeploymentHandler<PontoBlockReportsDeployment>().SendRequestAsync(pontoBlockReportsDeployment);
    }

    public static async Task<PontoBlockReportsService> 
        DeployContractAndGetServiceAsync(
            Nethereum.Web3.Web3 web3, 
            PontoBlockReportsDeployment pontoBlockReportsDeployment, 
            CancellationTokenSource? cancellationTokenSource = null)
    {
        var receipt = await DeployContractAndWaitForReceiptAsync(web3, pontoBlockReportsDeployment, cancellationTokenSource);
        return new PontoBlockReportsService(web3, receipt.ContractAddress);
    }

    protected Nethereum.Web3.IWeb3 Web3{ get; }

    public ContractHandler ContractHandler { get; }

    public PontoBlockReportsService(Nethereum.Web3.Web3 web3, string contractAddress)
    {
        Web3 = web3;
        ContractHandler = web3.Eth.GetContractHandler(contractAddress);
    }

    public PontoBlockReportsService(Nethereum.Web3.IWeb3 web3, string contractAddress)
    {
        Web3 = web3;
        ContractHandler = web3.Eth.GetContractHandler(contractAddress);
    }

    public Task<GetWorkTimeFromEmployeeBetweenTwoDatesOutputDTO> 
        GetWorkTimeFromEmployeeBetweenTwoDatesQueryAsync(
            GetWorkTimeFromEmployeeBetweenTwoDatesFunction getWorkTimeFromEmployeeBetweenTwoDatesFunction, 
            BlockParameter? blockParameter = null)
    {
        return ContractHandler
            .QueryDeserializingToObjectAsync
                <GetWorkTimeFromEmployeeBetweenTwoDatesFunction, GetWorkTimeFromEmployeeBetweenTwoDatesOutputDTO>
                (getWorkTimeFromEmployeeBetweenTwoDatesFunction, blockParameter);
    }

    public Task<GetWorkTimeFromEmployeeBetweenTwoDatesOutputDTO> 
        GetWorkTimeFromEmployeeBetweenTwoDatesQueryAsync(
            string employee, 
            BigInteger startDate, 
            BigInteger endDate, 
            BlockParameter? blockParameter = null)
    {
        var getWorkTimeFromEmployeeBetweenTwoDatesFunction = new GetWorkTimeFromEmployeeBetweenTwoDatesFunction();
        getWorkTimeFromEmployeeBetweenTwoDatesFunction.Employee = employee;
        getWorkTimeFromEmployeeBetweenTwoDatesFunction.StartDate = startDate;
        getWorkTimeFromEmployeeBetweenTwoDatesFunction.EndDate = endDate;
            
        return ContractHandler
            .QueryDeserializingToObjectAsync
                <GetWorkTimeFromEmployeeBetweenTwoDatesFunction, GetWorkTimeFromEmployeeBetweenTwoDatesOutputDTO>
                (getWorkTimeFromEmployeeBetweenTwoDatesFunction, blockParameter);
    }

    public Task<GetWorkTimesForAllEmployeesAtDateOutputDTO> 
        GetWorkTimesForAllEmployeesAtDateQueryAsync(
            GetWorkTimesForAllEmployeesAtDateFunction getWorkTimesForAllEmployeesAtDateFunction, 
            BlockParameter? blockParameter = null)
    {
        return ContractHandler
            .QueryDeserializingToObjectAsync
                <GetWorkTimesForAllEmployeesAtDateFunction, GetWorkTimesForAllEmployeesAtDateOutputDTO>
                (getWorkTimesForAllEmployeesAtDateFunction, blockParameter);
    }

    public Task<GetWorkTimesForAllEmployeesAtDateOutputDTO> 
        GetWorkTimesForAllEmployeesAtDateQueryAsync(
            BigInteger date, 
            BlockParameter? blockParameter = null)
    {
        var getWorkTimesForAllEmployeesAtDateFunction = new GetWorkTimesForAllEmployeesAtDateFunction();
        getWorkTimesForAllEmployeesAtDateFunction.Date = date;
            
        return ContractHandler
            .QueryDeserializingToObjectAsync
                <GetWorkTimesForAllEmployeesAtDateFunction, GetWorkTimesForAllEmployeesAtDateOutputDTO>
                (getWorkTimesForAllEmployeesAtDateFunction, blockParameter);
    }

    public Task<GetWorkTimesForAllEmployeesBetweenTwoDatesOutputDTO> 
        GetWorkTimesForAllEmployeesBetweenTwoDatesQueryAsync(
            GetWorkTimesForAllEmployeesBetweenTwoDatesFunction getWorkTimesForAllEmployeesBetweenTwoDatesFunction, 
            BlockParameter? blockParameter = null)
    {
        return ContractHandler
            .QueryDeserializingToObjectAsync
                <GetWorkTimesForAllEmployeesBetweenTwoDatesFunction, GetWorkTimesForAllEmployeesBetweenTwoDatesOutputDTO>
                (getWorkTimesForAllEmployeesBetweenTwoDatesFunction, blockParameter);
    }

    public Task<GetWorkTimesForAllEmployeesBetweenTwoDatesOutputDTO> 
        GetWorkTimesForAllEmployeesBetweenTwoDatesQueryAsync(
            BigInteger startDate, 
            BigInteger endDate, 
            BlockParameter? blockParameter = null)
    {
        var getWorkTimesForAllEmployeesBetweenTwoDatesFunction = new GetWorkTimesForAllEmployeesBetweenTwoDatesFunction();
        getWorkTimesForAllEmployeesBetweenTwoDatesFunction.StartDate = startDate;
        getWorkTimesForAllEmployeesBetweenTwoDatesFunction.EndDate = endDate;
            
        return ContractHandler
            .QueryDeserializingToObjectAsync
                <GetWorkTimesForAllEmployeesBetweenTwoDatesFunction, GetWorkTimesForAllEmployeesBetweenTwoDatesOutputDTO>
                (getWorkTimesForAllEmployeesBetweenTwoDatesFunction, blockParameter);
    }

    public Task<GetWorkTimesFromEmployeeAtDateOutputDTO> 
        GetWorkTimesFromEmployeeAtDateQueryAsync(
            GetWorkTimesFromEmployeeAtDateFunction getWorkTimesFromEmployeeAtDateFunction, 
            BlockParameter? blockParameter = null)
    {
        return ContractHandler
            .QueryDeserializingToObjectAsync
                <GetWorkTimesFromEmployeeAtDateFunction, GetWorkTimesFromEmployeeAtDateOutputDTO>
                (getWorkTimesFromEmployeeAtDateFunction, blockParameter);
    }

    public Task<GetWorkTimesFromEmployeeAtDateOutputDTO> 
        GetWorkTimesFromEmployeeAtDateQueryAsync(
            string employee, 
            BigInteger date, 
            BlockParameter? blockParameter = null)
    {
        var getWorkTimesFromEmployeeAtDateFunction = new GetWorkTimesFromEmployeeAtDateFunction();
        getWorkTimesFromEmployeeAtDateFunction.Employee = employee;
        getWorkTimesFromEmployeeAtDateFunction.Date = date;
            
        return ContractHandler
            .QueryDeserializingToObjectAsync
                <GetWorkTimesFromEmployeeAtDateFunction, GetWorkTimesFromEmployeeAtDateOutputDTO>
                (getWorkTimesFromEmployeeAtDateFunction, blockParameter);
    }
}