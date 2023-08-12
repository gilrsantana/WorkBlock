using System.Text.Json.Serialization;

namespace WorkBlockApp.DTOs;

public class ResultViewRequest<T>
{
    [JsonPropertyName("data")]
    public T? Data { get; set; }
    
    [JsonPropertyName("errors")]
    public string[]? Errors { get; set; }
}