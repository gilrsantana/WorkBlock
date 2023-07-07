using System.Text.Json.Serialization;

namespace WorkBlockApi.ViewModels;

public class EmployeeDateViewModel
{
    [JsonPropertyName("date")]
    public List<ulong>? Date { get; set; } 

    [JsonPropertyName("address")]
    public List<List<string>>? Address { get; set; }
    
    [JsonPropertyName("startWork")]
    public List<List<ulong>>? StartWork { get; set; }
    
    [JsonPropertyName("endWork")]
    public List<List<ulong>>? EndWork { get; set; }
    
    [JsonPropertyName("breakStartTime")]
    public List<List<ulong>>? BreakStartTime { get; set; }
    
    [JsonPropertyName("breakEndTime")]
    public List<List<ulong>>? BreakEndTime { get; set; }
}