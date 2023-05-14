namespace WorkBlockApi.ViewModels.Events.Administrator
{
    public class AdminAddedEventViewModel
    {
        public string AddressFrom { get; set; } = null!;
        public string AdministratorAddress { get; set; } = null!;
        public string AdministratorName { get; set; } = null!;
        public ulong AdministratorTaxId { get; set; }
        public DateTime Time { get; set; }
        public string HashTransaction { get; set; } = null!;

    }
}
