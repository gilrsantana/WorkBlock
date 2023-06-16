using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace WorkBlockApi.SmartContractsDefinitions.AdministratorContract.ContractDefinition;

public partial class Administrator : AdministratorBase { }
public class AdministratorBase 
{
    [Parameter("uint256", "idAdministrator", 1)]
    public virtual BigInteger IdAdministrator { get; set; }
    [Parameter("address", "administratorAddress", 2)]
    public virtual string AdministratorAddress { get; set; } = null!;
    [Parameter("uint256", "taxId", 3)]
    public virtual BigInteger TaxId { get; set; }
    [Parameter("string", "name", 4)]
    public virtual string Name { get; set; } = null!;
    [Parameter("uint8", "stateOf", 5)]
    public virtual byte StateOf { get; set; }
}