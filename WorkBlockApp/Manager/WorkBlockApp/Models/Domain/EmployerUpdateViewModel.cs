using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WorkBlockApp.Models.Domain;

public class EmployerUpdateViewModel
{
    [JsonPropertyName("addressFrom")]
    public string AddressFrom { get; set; } = null!;

    [JsonPropertyName("oldAddress")]
    public string OldAddress { get; set; } = null!;

    [JsonPropertyName("newAddress")]
    public string NewAddress { get; set; } = null!;

    [JsonPropertyName("employerName")]
    public string EmployerName { get; set; } = null!;

    [JsonPropertyName("employerTaxId")]
    public string EmployerTaxId { get; set; } = null!;

    [JsonPropertyName("employerLegalAddress")]
    public string EmployerLegalAddress { get; set; } = null!;

    [JsonPropertyName("time")]
    public DateTime Time { get; set; }

    [JsonPropertyName("hashTransaction")]
    public string HashTransaction { get; set; } = null!;
}
