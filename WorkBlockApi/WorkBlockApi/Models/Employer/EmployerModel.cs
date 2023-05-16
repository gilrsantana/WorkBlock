namespace WorkBlockApi.Models.Employer;

public class EmployerModel
{
    public uint IdAdministrator { get; set; }
    public string Address { get; set; } = null!;
    public string TaxId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string LegalAddress { get; set; } = null!;
}