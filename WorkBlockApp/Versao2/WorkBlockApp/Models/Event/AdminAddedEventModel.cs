using System.Text.Json.Serialization;

namespace WorkBlockApp.Models.Event;

public class AdminAddedEventModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("addressFrom")]
    public string AddressFrom { get; set; } = null!;

    [JsonPropertyName("administratorAddress")]
    public string AdministratorAddress { get; set; } = null!;

    [JsonPropertyName("administratorName")]
    public string AdministratorName { get; set; } = null!;

    [JsonPropertyName("administratorTaxId")]
    public string AdministratorTaxId { get; set; } = null!;

    [JsonPropertyName("time")]
    public DateTime Time { get; set; }

    [JsonPropertyName("hashTransaction")]
    public string HashTransaction { get; set; } = null!;
}