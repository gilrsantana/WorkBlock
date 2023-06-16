using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace WorkBlockApi.SmartContractsDefinitions.PontoBlock.ContractDefinition;

public partial class EmployeeRecord : EmployeeRecordBase { }

public class EmployeeRecordBase 
{
    [Parameter("uint256", "startWork", 1)]
    public virtual BigInteger StartWork { get; set; }
    [Parameter("uint256", "endWork", 2)]
    public virtual BigInteger EndWork { get; set; }
    [Parameter("uint256", "breakStartTime", 3)]
    public virtual BigInteger BreakStartTime { get; set; }
    [Parameter("uint256", "breakEndTime", 4)]
    public virtual BigInteger BreakEndTime { get; set; }
}