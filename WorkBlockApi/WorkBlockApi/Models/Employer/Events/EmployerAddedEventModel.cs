namespace WorkBlockApi.Models.Employer.Events;

public class EmployerAddedEventModel
{
    public Guid Id { get; set; }
    public string AddressFrom { get; set; } = null!;
    public string EmployerAddress { get; set; } = null!;
    public string EmployerName { get; set; } = null!;
    public string EmployerTaxId { get; set; } = null!;
    public string EmployerLegalAddress { get; set; } = null!;
    public DateTime Time { get; set; }
    public string HashTransaction { get; set; } = null!;
}