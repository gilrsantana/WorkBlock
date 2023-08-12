using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WorkBlockApp.Models.DataAnnotation;

namespace WorkBlockApp.Models.Domain;

public class AdministratorModel
{
    [Required(ErrorMessage = "O campo nome é obrigatório")]
    [MinLength(3, ErrorMessage = "O campo nome deve ter no mínimo 3 caracteres")]
    [Display(Name = "Nome Completo", Prompt = "Nome do administrador"), StringLength(100)]
    [JsonPropertyName("name")]
    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "O campo CPF é obrigatório")]
    [RegularExpression("\\d{3}\\.\\d{3}\\.\\d{3}-\\d{2}", ErrorMessage = "O campo CPF deve estar em formato correto")]
    [RequiredValidCpf(ErrorMessage = "O CPF informado não é válido")]
    [Display(Name = "CPF", Prompt = "000.000.000-00"), StringLength(14)]
    [JsonPropertyName("taxId")]
    public string Cpf { get; set; } = null!;

    [Required(ErrorMessage = "O campo endereço de carteira é obrigatório")]
    [RegularExpression("^0x[a-fA-F0-9]{40}$", ErrorMessage = "O  endereço de carteira deve estar em formato correto")]
    [Display(Name = "Endereço de Carteira", Prompt = "0x71C7...9553"), StringLength(42)]
    [JsonPropertyName("address")]
    public string Carteira { get; set; } = null!;
    
    [Range(0, 1, ErrorMessage = "O campo estado deve ser 0 ou 1")]
    [JsonPropertyName("state")]
    public int Ativo { get; set; }
}
