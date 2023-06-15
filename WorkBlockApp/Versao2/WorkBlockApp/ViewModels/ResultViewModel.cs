using System.Text.Json.Serialization;

namespace WorkBlockApp.ViewModels;

public class ResultViewModel<T>
{
    [JsonPropertyName("data")]
    public T? Data { get; private set; }
    
    [JsonPropertyName("errors")]
    public List<string> Errors { get; private set; } = new();

    public ResultViewModel(T data, List<string> errors)
    {
        Data = data;
        Errors = errors;
    }

    public ResultViewModel(T data)
    {
        Data = data;
    }

    public ResultViewModel(List<string> errors) => Errors = errors;

    public ResultViewModel(string error) => Errors.Add(error);
}