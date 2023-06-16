using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace WorkBlockApi.SmartContractsDefinitions.EmployeeContract.ContractDefinition;

public partial class Employee : EmployeeBase { }
public class EmployeeBase 
{
    [Parameter("uint256", "idEmployee", 1)]
    public virtual BigInteger IdEmployee { get; set; }
    [Parameter("address", "employeeAddress", 2)]
    public virtual string EmployeeAddress { get; set; } = null!;
    [Parameter("uint256", "taxId", 3)]
    public virtual BigInteger TaxId { get; set; }
    [Parameter("string", "name", 4)]
    public virtual string Name { get; set; } = null!;
    [Parameter("uint256", "begginingWorkDay", 5)]
    public virtual BigInteger BegginingWorkDay { get; set; }
    [Parameter("uint256", "endWorkDay", 6)]
    public virtual BigInteger EndWorkDay { get; set; }
    [Parameter("uint8", "stateOf", 7)]
    public virtual byte StateOf { get; set; }
    [Parameter("address", "employerAddress", 8)]
    public virtual string EmployerAddress { get; set; } = null!;
}