using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WorkBlockApp.Models.Domain;

public class AdministratorUpdateModel : AdministratorModel
{
    [Required(ErrorMessage = "O campo novo endereço de carteira é obrigatório")]
    [RegularExpression("^0x[a-fA-F0-9]{40}$", ErrorMessage = "O  endereço de carteira deve estar em formato correto")]
    [Display(Name = "Novo Endereço de Carteira", Prompt = "0x71C7...9553"), StringLength(42)]
    [JsonPropertyName("address")]
    public string NovaCarteira { get; set; } = null!;
}