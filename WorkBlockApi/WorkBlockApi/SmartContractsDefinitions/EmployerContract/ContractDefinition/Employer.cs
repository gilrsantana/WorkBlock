using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace WorkBlockApi.SmartContractsDefinitions.EmployerContract.ContractDefinition;

public partial class Employer : EmployerBase { }
public class EmployerBase 
{
    [Parameter("uint256", "idEmployer", 1)]
    public virtual BigInteger IdEmployer { get; set; }
    [Parameter("address", "employerAddress", 2)]
    public virtual string EmployerAddress { get; set; } = null!;
    [Parameter("uint256", "taxId", 3)]
    public virtual BigInteger TaxId { get; set; }
    [Parameter("string", "name", 4)]
    public virtual string Name { get; set; } = null!;
    [Parameter("string", "legalAddress", 5)]
    public virtual string LegalAddress { get; set; } = null!;
}