using System.Text.Json.Serialization;

namespace WorkBlockApp.ViewModels.VMEmployee
{
    public class EmployeeIndexViewModel
    {
        [JsonPropertyName("address")]
        public string Carteira { get; set; } = null!;

        [JsonPropertyName("taxId")]
        public string Pis { get; set; } = null!;

        [JsonPropertyName("name")]
        public string Nome { get; set; } = null!;

        [JsonPropertyName("begginingWorkDay")]
        public int InicioJornada { get; set; }

        [JsonPropertyName("endWorkDay")]
        public int FimJornada { get; set; }

        [JsonPropertyName("stateOf")]
        public int Ativo { get; set; }

        [JsonPropertyName("employerAddress")]
        public string Empregador { get; set; } = null!;
    }
}