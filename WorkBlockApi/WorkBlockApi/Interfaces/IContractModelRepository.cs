using WorkBlockApi.Model;

namespace WorkBlockApi.Interfaces;

public interface IContractModelRepository
{
    Task<ContractModel?> GetByNameAsync(string contractName);
}