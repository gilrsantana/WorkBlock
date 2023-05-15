namespace WorkBlockApi.Models.Administrator.Events;

public class AdminUpdatedEventModel
{
    public Guid Id { get; set; }
    public string AddressFrom { get; set; } = null!;
    public string OldAddress { get; set; } = null!;
    public string NewAddress { get; set; } = null!;
    public string AdministratorName { get; set; } = null!;
    public string AdministratorTaxId { get; set; } = null!;
    public byte State { get; set; }
    public DateTime Time { get; set; }
    public string HashTransaction { get; set; } = null!;
}
