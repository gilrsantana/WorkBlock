namespace WorkBlockApi.Model;
using Nethereum;
public class AdministratorModel
{
    public uint IdAdministrator { get; set; }
    public string Address { get; set; }
    public uint  TaxId { get; set; }
    public string Name { get; set; }
    public int State { get; set; }
}
