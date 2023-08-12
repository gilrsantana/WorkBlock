namespace WorkBlockApp.ViewModels.VMAdministrator;

public class AdminAddViewModel
{
    public string Address { get; set; } = null!;
    public string Name { get; set; } = null!;
    public ulong TaxId { get; set; }
    public byte State { get; set; }
}