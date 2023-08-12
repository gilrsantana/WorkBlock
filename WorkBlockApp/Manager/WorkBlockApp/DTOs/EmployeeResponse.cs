using System.Text.Json.Serialization;

namespace WorkBlockApp.DTOs;

public class EmployeeResponse
{
    [JsonPropertyName("name")]
    public string Nome { get; set; } = null!;

    [JsonPropertyName("taxId")]
    public string Pis { get; set; } = null!;

    [JsonPropertyName("address")]
    public string Carteira { get; set; } = null!;

    [JsonPropertyName("begginingWorkDay")]
    public ulong InicioJornada { get; set; }

    [JsonPropertyName("endWorkDay")]
    public ulong FimJornada { get; set; }

    [JsonPropertyName("employerAddress")]
    public string Empregador { get; set; } = null!;

    [JsonPropertyName("stateOf")]
    public int Ativo { get; set; }
}
