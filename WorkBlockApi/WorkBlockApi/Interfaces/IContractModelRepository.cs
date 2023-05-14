using WorkBlockApi.Models;

namespace WorkBlockApi.Interfaces;

public interface IContractModelRepository
{
    Task<ContractModel?> GetByNameAsync(string contractName);
    Task<List<ContractModel>> GetAllContractsAsync();
}