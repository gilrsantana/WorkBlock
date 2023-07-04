using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WorkBlockApp.ViewModels.VMReports
{
    public class ReportAtDateOutPutViewModel
    {
        [JsonPropertyName("address")]
        public string Carteira { get; set; } = null!;

        [JsonPropertyName("timestamp")]
        public ulong Timestamp { get; set; }
    }
}