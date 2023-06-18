using System.Text.Json.Serialization;

namespace WorkBlockApp.ViewModels.VMEmployer;

public class EmployerIndexViewModel
{
    [JsonPropertyName("name")]
    public string Nome { get; set; } = null!;

    [JsonPropertyName("taxId")]
    public string Cnpj { get; set; } = null!;

    [JsonPropertyName("address")]
    public string Carteira { get; set; } = null!;

    [JsonPropertyName("legalAddress")]
    public string Endereco { get; set; } = null!;
}
