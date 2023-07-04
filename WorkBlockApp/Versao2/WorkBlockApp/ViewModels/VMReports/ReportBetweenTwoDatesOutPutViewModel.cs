using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WorkBlockApp.ViewModels.VMReports;

public class ReportBetweenTwoDatesOutPutViewModel
{
    [JsonPropertyName("address")]
    public string Carteira { get; set; } = null!;

    [JsonPropertyName("startTimestamp")]
    public ulong PeriodoInicio { get; set; }

    [JsonPropertyName("endTimestamp")]
    public ulong PeriodoFim { get; set; }
}
