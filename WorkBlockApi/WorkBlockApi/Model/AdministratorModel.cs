namespace WorkBlockApi.Model;

public class AdministratorModel
{
    public uint IdAdministrator { get; set; }
    public string Address { get; set; } = null!;
    public uint  TaxId { get; set; }
    public string Name { get; set; } = null!;
    public int State { get; set; }
}
