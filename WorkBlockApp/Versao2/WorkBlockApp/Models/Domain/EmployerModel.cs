using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WorkBlockApp.Models.DataAnnotation;
using WorkBlockApp.Models.ValueObjects;

namespace WorkBlockApp.Models.Domain
{
    public class EmployerModel
    {
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [MinLength(3, ErrorMessage = "O campo nome deve ter no mínimo 3 caracteres")]
        [Display(Name = "Nome Completo", Prompt = "Nome do empregador"), StringLength(100)]
        [JsonPropertyName("name")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O campo CNPJ é obrigatório")]
        [RegularExpression("\\d{2}\\.\\d{3}\\.\\d{3}/\\d{4}-\\d{2}", ErrorMessage = "O campo CNPJ deve estar em formato correto")]
        [RequiredValidCnpj(ErrorMessage = "O CNPJ informado não é válido")]
        [Display(Name = "CNPJ", Prompt = "00.000.000.0000-00"), StringLength(18)]
        [JsonPropertyName("taxId")]
        public string Cnpj { get; set; } = null!;

        [Required(ErrorMessage = "O campo endereço de carteira é obrigatório")]
        [RegularExpression("^0x[a-fA-F0-9]{40}$", ErrorMessage = "O  endereço de carteira deve estar em formato correto")]
        [Display(Name = "Endereço de Carteira", Prompt = "0x71C7...9553"), StringLength(42)]
        [JsonPropertyName("address")]
        public string Carteira { get; set; } = null!;

        [JsonPropertyName("legalAddress")]
        public AddressModel Endereco { get; set; } = null!;
    }
}
