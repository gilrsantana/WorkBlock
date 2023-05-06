namespace WorkBlockApi.Interfaces;

public interface IContractModel
{
    Guid Id { get; set; }
    string Name { get; set; }
    string AddressContract { get; set; }
    string Abi { get; set; }
    string Bytecode { get; set; }
    DateTime CreatedAt { get; set; }
}