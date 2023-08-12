using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WorkBlockApp.Models.Domain;

public class EmployeeUpdateViewModel
{
    [JsonPropertyName("addressFrom")]
    public string AddressFrom { get; set; } = null!;

    [JsonPropertyName("oldAddress")]
    public string OldAddress { get; set; } = null!;

    [JsonPropertyName("newAddress")]
    public string NewAddress { get; set; } = null!;

    [JsonPropertyName("employeeName")]
    public string EmployeeName { get; set; } = null!;

    [JsonPropertyName("employeeTaxId")]
    public string EmployeeTaxId { get; set; } = null!;

    [JsonPropertyName("employeeBegginingWorkDay")]
    public TimeOnly EmployeeBegginingWorkDay { get; set; }

    [JsonPropertyName("employeeEndWorkDay")]
    public TimeOnly EmployeeEndWorkDay { get; set; }

    [JsonPropertyName("employeerAddress")]
    public string EmployerAddress { get; set; } = null!;

    [JsonPropertyName("state")]
    public byte State { get; set; }

    [JsonPropertyName("Time")]
    public DateTime Time { get; set; }

    [JsonPropertyName("hashTransaction")]
    public string HashTransaction { get; set; } = null!;
}
