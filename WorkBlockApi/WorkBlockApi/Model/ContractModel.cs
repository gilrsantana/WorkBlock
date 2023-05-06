using WorkBlockApi.Interfaces;

namespace WorkBlockApi.Model;

public class ContractModel : IContractModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string AddressContract { get; set; } = null!;
    public string Abi { get; set; } = null!;
    public string Bytecode { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

}