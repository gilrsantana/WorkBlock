namespace WorkBlockApi.Models.Administrator.Events;

public class AdminAddedEventModel 
{
    public Guid Id { get; set; }
    public string AddressFrom { get; set; } = null!;
    public string AdministratorAddress { get; set; } = null!;
    public string AdministratorName { get; set; } = null!;
    public string AdministratorTaxId { get; set; } = null!;
    public DateTime Time { get; set; }
    public string HashTransaction { get; set; } = null!;
}