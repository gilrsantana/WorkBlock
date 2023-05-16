namespace WorkBlockApi.Models.Administrator;

public class AdministratorModel
{
    public uint IdAdministrator { get; set; }
    public string Address { get; set; } = null!;
    public string TaxId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int State { get; set; }
}
