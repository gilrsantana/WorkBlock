using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace WorkBlockApi.SmartContractsDefinitions.EmployerContract.ContractDefinition;

public partial class EmployerContractDeployment : EmployerContractDeploymentBase
{
    public EmployerContractDeployment() : base(BYTECODE) { }
    public EmployerContractDeployment(string byteCode) : base(byteCode) { }
}

public class EmployerContractDeploymentBase : ContractDeploymentMessage
{
    public static string BYTECODE = "608060405234801561001057600080fd5b5060405161179838038061179883398101604081905261002f91610054565b600080546001600160a01b0319166001600160a01b0392909216919091179055610084565b60006020828403121561006657600080fd5b81516001600160a01b038116811461007d57600080fd5b9392505050565b611705806100936000396000f3fe608060405234801561001057600080fd5b50600436106100625760003560e01c806314c671d6146100675780632907e0f8146100855780632a57272c146100a55780637a787125146100c8578063a39fa249146100db578063c4064faf146100f0575b600080fd5b61006f610103565b60405161007c919061120c565b60405180910390f35b61009861009336600461126e565b61039c565b60405161007c9190611287565b6100b86100b33660046112b8565b61059e565b604051901515815260200161007c565b6100986100d63660046112b8565b61068f565b6100ee6100e9366004611376565b6108f5565b005b6100ee6100fe366004611405565b610d97565b60005460405163debcf75d60e01b81523360048201526060916001600160a01b03169063debcf75d90602401602060405180830381865afa15801561014c573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052508101906101709190611483565b6101955760405162461bcd60e51b815260040161018c906114a5565b60405180910390fd5b60025460009067ffffffffffffffff8111156101b3576101b36112d3565b6040519080825280602002602001820160405280156101ec57816020015b6101d9611127565b8152602001906001900390816101d15790505b50905060005b60025481101561039657600081815260016020818152604092839020835160a08101855281548152928101546001600160a01b031691830191909152600281015492820192909252600382018054919291606084019190610252906114f0565b80601f016020809104026020016040519081016040528092919081815260200182805461027e906114f0565b80156102cb5780601f106102a0576101008083540402835291602001916102cb565b820191906000526020600020905b8154815290600101906020018083116102ae57829003601f168201915b505050505081526020016004820180546102e4906114f0565b80601f0160208091040260200160405190810160405280929190818152602001828054610310906114f0565b801561035d5780601f106103325761010080835404028352916020019161035d565b820191906000526020600020905b81548152906001019060200180831161034057829003601f168201915b5050505050815250508282815181106103785761037861152a565b6020026020010181905250808061038e90611540565b9150506101f2565b50905090565b6103a4611127565b60005460405163debcf75d60e01b81523360048201526001600160a01b039091169063debcf75d90602401602060405180830381865afa1580156103ec573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052508101906104109190611483565b61042c5760405162461bcd60e51b815260040161018c906114a5565b600082815260016020818152604092839020835160a08101855281548152928101546001600160a01b031691830191909152600281015492820192909252600382018054919291606084019190610482906114f0565b80601f01602080910402602001604051908101604052809291908181526020018280546104ae906114f0565b80156104fb5780601f106104d0576101008083540402835291602001916104fb565b820191906000526020600020905b8154815290600101906020018083116104de57829003601f168201915b50505050508152602001600482018054610514906114f0565b80601f0160208091040260200160405190810160405280929190818152602001828054610540906114f0565b801561058d5780601f106105625761010080835404028352916020019161058d565b820191906000526020600020905b81548152906001019060200180831161057057829003601f168201915b50505050508152505090505b919050565b6000805460405163debcf75d60e01b81523360048201526001600160a01b039091169063debcf75d90602401602060405180830381865afa1580156105e7573d6000803e3d6000fd5b505050506040513d601f19601f8201168201806040525081019061060b9190611483565b6106275760405162461bcd60e51b815260040161018c906114a5565b60005b60025481101561068657826001600160a01b0316600282815481106106515761065161152a565b6000918252602090912001546001600160a01b0316036106745750600192915050565b8061067e81611540565b91505061062a565b50600092915050565b610697611127565b60005460405163debcf75d60e01b81523360048201526001600160a01b039091169063debcf75d90602401602060405180830381865afa1580156106df573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052508101906107039190611483565b61071f5760405162461bcd60e51b815260040161018c906114a5565b610727611127565b60005b6002548110156108ee57836001600160a01b0316600282815481106107515761075161152a565b6000918252602090912001546001600160a01b0316036108dc57600081815260016020818152604092839020835160a08101855281548152928101546001600160a01b0316918301919091526002810154928201929092526003820180549192916060840191906107c1906114f0565b80601f01602080910402602001604051908101604052809291908181526020018280546107ed906114f0565b801561083a5780601f1061080f5761010080835404028352916020019161083a565b820191906000526020600020905b81548152906001019060200180831161081d57829003601f168201915b50505050508152602001600482018054610853906114f0565b80601f016020809104026020016040519081016040528092919081815260200182805461087f906114f0565b80156108cc5780601f106108a1576101008083540402835291602001916108cc565b820191906000526020600020905b8154815290600101906020018083116108af57829003601f168201915b50505050508152505091506108ee565b806108e681611540565b91505061072a565b5092915050565b60005460405163debcf75d60e01b81523360048201526001600160a01b039091169063debcf75d90602401602060405180830381865afa15801561093d573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052508101906109619190611483565b61097d5760405162461bcd60e51b815260040161018c906114a5565b846109878161059e565b6109ca5760405162461bcd60e51b815260206004820152601460248201527322b6b83637bcb2b9103737ba1032bc34b9ba399760611b604482015260640161018c565b6001600160a01b038516610a155760405162461bcd60e51b815260206004820152601260248201527120b2323932b9b9903737ba1033b4bb32b71760711b604482015260640161018c565b83600003610a585760405162461bcd60e51b815260206004820152601060248201526f2a30bc24b2103737ba1033b4bb32b71760811b604482015260640161018c565b6040805160008152602081018083528151902091610a7891869101611567565b6040516020818303038152906040528051906020012003610acd5760405162461bcd60e51b815260206004820152600f60248201526e2730b6b2903737ba1033b4bb32b71760891b604482015260640161018c565b6040805160008152602081018083528151902091610aed91859101611567565b6040516020818303038152906040528051906020012003610b4b5760405162461bcd60e51b81526020600482015260186024820152772632b3b0b61030b2323932b9b9903737ba1033b4bb32b71760411b604482015260640161018c565b60008080805b600254811015610bac57896001600160a01b031660028281548110610b7857610b7861152a565b6000918252602090912001546001600160a01b031603610b9a57809150610bac565b80610ba481611540565b915050610b51565b50600081815260016020819052604090912001546001600160a01b03898116911614610bf55760008181526001602081905260409091208101549093506001600160a01b031691505b6040805160a0810182528281526001600160a01b038a811660208084019182528385018c8152606085018c8152608086018c905260008881526001938490529690962085518155925191830180546001600160a01b03191692909416919091179092559051600282015591519091906003820190610c7390826115d2565b5060808201516004820190610c8890826115d2565b509050508215610d315760005b600254811015610d2f57826001600160a01b031660028281548110610cbc57610cbc61152a565b6000918252602090912001546001600160a01b031603610d1d578860028281548110610cea57610cea61152a565b9060005260206000200160006101000a8154816001600160a01b0302191690836001600160a01b03160217905550610d2f565b80610d2781611540565b915050610c95565b505b876001600160a01b0316896001600160a01b0316336001600160a01b03167fdef1b586eb40adb84a80b7fccc0f1e863d5bed1c43112bad185c84d73280ccb3898b8a42604051610d849493929190611692565b60405180910390a4505050505050505050565b60005460405163debcf75d60e01b81523360048201526001600160a01b039091169063debcf75d90602401602060405180830381865afa158015610ddf573d6000803e3d6000fd5b505050506040513d601f19601f82011682018060405250810190610e039190611483565b610e1f5760405162461bcd60e51b815260040161018c906114a5565b83610e298161059e565b15610e765760405162461bcd60e51b815260206004820152601860248201527f456d706c6f79657220616c7265616479206578697374732e0000000000000000604482015260640161018c565b83600003610eb95760405162461bcd60e51b815260206004820152601060248201526f2a30bc24b2103737ba1033b4bb32b71760811b604482015260640161018c565b6040805160008152602081018083528151902091610ed991869101611567565b6040516020818303038152906040528051906020012003610f2e5760405162461bcd60e51b815260206004820152600f60248201526e2730b6b2903737ba1033b4bb32b71760891b604482015260640161018c565b6040805160008152602081018083528151902091610f4e91859101611567565b6040516020818303038152906040528051906020012003610fac5760405162461bcd60e51b81526020600482015260186024820152772632b3b0b61030b2323932b9b9903737ba1033b4bb32b71760411b604482015260640161018c565b6001600160a01b038516610ff75760405162461bcd60e51b815260206004820152601260248201527120b2323932b9b9903737ba1033b4bb32b71760711b604482015260640161018c565b6040805160a081018252600280548083526001600160a01b0389811660208086019182528587018b8152606087018b8152608088018b905260009586526001928390529790942086518155915190820180546001600160a01b03191691909316179091559051918101919091559151909190600382019061107890826115d2565b506080820151600482019061108d90826115d2565b5050600280546001810182556000919091527f405787fa12a823e0f2b7631cc41b3ba8828b3321ca811111fa75cd3aa3bb5ace0180546001600160a01b0319166001600160a01b03881690811790915560405190915033907f13397531ff38d5bb86412acf9831561ee8485875460b167171b907f5fc0a182a90611118908790899088904290611692565b60405180910390a35050505050565b6040518060a001604052806000815260200160006001600160a01b031681526020016000815260200160608152602001606081525090565b60005b8381101561117a578181015183820152602001611162565b50506000910152565b6000815180845261119b81602086016020860161115f565b601f01601f19169290920160200192915050565b8051825260018060a01b036020820151166020830152604081015160408301526000606082015160a060608501526111ea60a0850182611183565b9050608083015184820360808601526112038282611183565b95945050505050565b6000602080830181845280855180835260408601915060408160051b870101925083870160005b8281101561126157603f1988860301845261124f8583516111af565b94509285019290850190600101611233565b5092979650505050505050565b60006020828403121561128057600080fd5b5035919050565b60208152600061129a60208301846111af565b9392505050565b80356001600160a01b038116811461059957600080fd5b6000602082840312156112ca57600080fd5b61129a826112a1565b634e487b7160e01b600052604160045260246000fd5b600082601f8301126112fa57600080fd5b813567ffffffffffffffff80821115611315576113156112d3565b604051601f8301601f19908116603f0116810190828211818310171561133d5761133d6112d3565b8160405283815286602085880101111561135657600080fd5b836020870160208301376000602085830101528094505050505092915050565b600080600080600060a0868803121561138e57600080fd5b611397866112a1565b94506113a5602087016112a1565b935060408601359250606086013567ffffffffffffffff808211156113c957600080fd5b6113d589838a016112e9565b935060808801359150808211156113eb57600080fd5b506113f8888289016112e9565b9150509295509295909350565b6000806000806080858703121561141b57600080fd5b611424856112a1565b935060208501359250604085013567ffffffffffffffff8082111561144857600080fd5b611454888389016112e9565b9350606087013591508082111561146a57600080fd5b50611477878288016112e9565b91505092959194509250565b60006020828403121561149557600080fd5b8151801515811461129a57600080fd5b6020808252602b908201527f53656e646572206d7573742062652061646d696e6973747261746f7220616e6460408201526a1031329030b1ba34bb329760a91b606082015260800190565b600181811c9082168061150457607f821691505b60208210810361152457634e487b7160e01b600052602260045260246000fd5b50919050565b634e487b7160e01b600052603260045260246000fd5b60006001820161156057634e487b7160e01b600052601160045260246000fd5b5060010190565b6000825161157981846020870161115f565b9190910192915050565b601f8211156115cd57600081815260208120601f850160051c810160208610156115aa5750805b601f850160051c820191505b818110156115c9578281556001016115b6565b5050505b505050565b815167ffffffffffffffff8111156115ec576115ec6112d3565b611600816115fa84546114f0565b84611583565b602080601f831160018114611635576000841561161d5750858301515b600019600386901b1c1916600185901b1785556115c9565b600085815260208120601f198616915b8281101561166457888601518255948401946001909101908401611645565b50858210156116825787850151600019600388901b60f8161c191681555b5050505050600190811b01905550565b6080815260006116a56080830187611183565b85602084015282810360408401526116bd8186611183565b9150508260608301529594505050505056fea2646970667358221220edb797d4cfb31f8517d788cada737f8c61d33a13c6f4cfc0ab747d2a2c85974a64736f6c63430008130033";
    public EmployerContractDeploymentBase() : base(BYTECODE) { }
    public EmployerContractDeploymentBase(string byteCode) : base(byteCode) { }
    [Parameter("address", "_adm", 1)]
    public virtual string Adm { get; set; } = null!;
}

public partial class AddEmployerFunction : AddEmployerFunctionBase { }

[Function("addEmployer")]
public class AddEmployerFunctionBase : FunctionMessage
{
    [Parameter("address", "_address", 1)]
    public virtual string Address { get; set; } = null!;
    [Parameter("uint256", "_taxId", 2)]
    public virtual BigInteger TaxId { get; set; }
    [Parameter("string", "_name", 3)]
    public virtual string Name { get; set; } = null!;
    [Parameter("string", "_legalAddress", 4)]
    public virtual string LegalAddress { get; set; } = null!;
}

public partial class CheckIfEmployerExistsFunction : CheckIfEmployerExistsFunctionBase { }

[Function("checkIfEmployerExists", "bool")]
public class CheckIfEmployerExistsFunctionBase : FunctionMessage
{
    [Parameter("address", "_address", 1)]
    public virtual string Address { get; set; } = null!;
}

public partial class GetAllEmployersFunction : GetAllEmployersFunctionBase { }

[Function("getAllEmployers", typeof(GetAllEmployersOutputDTO))]
public class GetAllEmployersFunctionBase : FunctionMessage
{

}

public partial class GetEmployerByAddressFunction : GetEmployerByAddressFunctionBase { }

[Function("getEmployerByAddress", typeof(GetEmployerByAddressOutputDTO))]
public class GetEmployerByAddressFunctionBase : FunctionMessage
{
    [Parameter("address", "_address", 1)]
    public virtual string Address { get; set; } = null!;
}

public partial class GetEmployerByIdFunction : GetEmployerByIdFunctionBase { }

[Function("getEmployerById", typeof(GetEmployerByIdOutputDTO))]
public class GetEmployerByIdFunctionBase : FunctionMessage
{
    [Parameter("uint256", "_id", 1)]
    public virtual BigInteger Id { get; set; }
}

public partial class UpdateEmployerFunction : UpdateEmployerFunctionBase { }

[Function("updateEmployer")]
public class UpdateEmployerFunctionBase : FunctionMessage
{
    [Parameter("address", "_addressKey", 1)]
    public virtual string AddressKey { get; set; } = null!;
    [Parameter("address", "_address", 2)]
    public virtual string Address { get; set; } = null!;
    [Parameter("uint256", "_taxId", 3)]
    public virtual BigInteger TaxId { get; set; }
    [Parameter("string", "_name", 4)]
    public virtual string Name { get; set; } = null!;
    [Parameter("string", "_legalAddress", 5)]
    public virtual string LegalAddress { get; set; } = null!;
}

public partial class EmployerAddedEventDTO : EmployerAddedEventDTOBase { }

[Event("EmployerAdded")]
public class EmployerAddedEventDTOBase : IEventDTO
{
    [Parameter("address", "from_", 1, true )]
    public virtual string From { get; set; } = null!;
    [Parameter("address", "address_", 2, true )]
    public virtual string Address { get; set; } = null!;
    [Parameter("string", "name_", 3, false )]
    public virtual string Name { get; set; } = null!;
    [Parameter("uint256", "taxId_", 4, false )]
    public virtual BigInteger Taxid { get; set; }
    [Parameter("string", "legalAddress_", 5, false )]
    public virtual string Legaladdress { get; set; } = null!;
    [Parameter("uint256", "timestamp_", 6, false )]
    public virtual BigInteger Timestamp { get; set; }
}

public partial class EmployerUpdatedEventDTO : EmployerUpdatedEventDTOBase { }

[Event("EmployerUpdated")]
public class EmployerUpdatedEventDTOBase : IEventDTO
{
    [Parameter("address", "from_", 1, true )]
    public virtual string From { get; set; } = null!;
    [Parameter("address", "oldAddress_", 2, true )]
    public virtual string Oldaddress { get; set; } = null!;
    [Parameter("address", "newAddress_", 3, true )]
    public virtual string Newaddress { get; set; } = null!;
    [Parameter("string", "name_", 4, false )]
    public virtual string Name { get; set; } = null!;
    [Parameter("uint256", "taxId_", 5, false )]
    public virtual BigInteger Taxid { get; set; }
    [Parameter("string", "legaAddress_", 6, false )]
    public virtual string Legaaddress { get; set; } = null!;
    [Parameter("uint256", "timestamp_", 7, false )]
    public virtual BigInteger Timestamp { get; set; }
}



public partial class CheckIfEmployerExistsOutputDTO : CheckIfEmployerExistsOutputDTOBase { }

[FunctionOutput]
public class CheckIfEmployerExistsOutputDTOBase : IFunctionOutputDTO 
{
    [Parameter("bool", "", 1)]
    public virtual bool ReturnValue1 { get; set; }
}

public partial class GetAllEmployersOutputDTO : GetAllEmployersOutputDTOBase { }

[FunctionOutput]
public class GetAllEmployersOutputDTOBase : IFunctionOutputDTO 
{
    [Parameter("tuple[]", "", 1)]
    public virtual List<Employer> ReturnValue1 { get; set; } = null!;
}

public partial class GetEmployerByAddressOutputDTO : GetEmployerByAddressOutputDTOBase { }

[FunctionOutput]
public class GetEmployerByAddressOutputDTOBase : IFunctionOutputDTO 
{
    [Parameter("tuple", "", 1)]
    public virtual Employer ReturnValue1 { get; set; } = null!;
}

public partial class GetEmployerByIdOutputDTO : GetEmployerByIdOutputDTOBase { }

[FunctionOutput]
public class GetEmployerByIdOutputDTOBase : IFunctionOutputDTO 
{
    [Parameter("tuple", "", 1)]
    public virtual Employer ReturnValue1 { get; set; } = null!;
}