namespace WorkBlockApp.ViewModels.VMReports
{
    public class ReportAtDateOutPutViewModel
    {
        public string Carteira { get; set; } = null!;
        public ulong StartTimestamp { get; set; }
        public ulong EndTimestamp { get; set; }
    }
}