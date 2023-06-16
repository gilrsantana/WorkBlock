using System.Text.Json.Serialization;

namespace WorkBlockApp.Models.Domain;

public class AdministratorUpdateViewModel
{
    [JsonPropertyName("oldAddress")]
    public string OldAddress { get; set; } = null!;

    [JsonPropertyName("newAddress")]
    public string NewAddress { get; set; } = null!;

    [JsonPropertyName("administratorName")]
    public string AdministratorName { get; set; } = null!;

    [JsonPropertyName("administratorTaxId")]
    public string AdministratorTaxId { get; set; } = null!;

    [JsonPropertyName("state")]
    public byte State { get; set; }

    [JsonPropertyName("time")]
    public DateTime Time { get; set; }

    [JsonPropertyName("hashTransaction")]
    public string HashTransaction { get; set; } = null!;
}