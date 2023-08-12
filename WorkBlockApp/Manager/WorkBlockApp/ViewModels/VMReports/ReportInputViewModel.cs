using System.Numerics;
using System.Text.Json.Serialization;

namespace WorkBlockApp.ViewModels.VMReports;

public class ReportInputViewModel
{
    [JsonPropertyName("date")]
    public ulong Date { get; set; }

    [JsonPropertyName("startWork")]
    public BigInteger StartWork { get; set; }

    [JsonPropertyName("endWork")]
    public BigInteger EndWork { get; set; }

    [JsonPropertyName("breakStartTime")]
    public BigInteger BreakStartTime { get; set; }

    [JsonPropertyName("breakEndTime")]
    public BigInteger BreakEndTime { get; set; } 
}


    