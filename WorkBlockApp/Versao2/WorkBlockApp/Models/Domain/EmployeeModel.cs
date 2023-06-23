using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WorkBlockApp.Models.DataAnnotation;

namespace WorkBlockApp.Models.Domain;

public class EmployeeModel
{
    [Required(ErrorMessage = "O campo nome é obrigatório")]
    [MinLength(3, ErrorMessage = "O campo nome deve ter no mínimo 3 caracteres")]
    [Display(Name = "Nome Completo", Prompt = "Nome do colaborador"), StringLength(100)]
    [JsonPropertyName("name")]
    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "O campo PIS é obrigatório")]
    [RegularExpression("\\d{3}\\.\\d{5}\\.\\d{2}-\\d{1}", ErrorMessage = "O campo PIS deve estar em formato correto")]
    [RequiredValidPis(ErrorMessage = "O PIS informado não é válido")]
    [Display(Name = "PIS", Prompt = "000.00000.00-0"), StringLength(14)]
    [JsonPropertyName("taxId")]
    public string Pis { get; set; } = null!;

    [Required(ErrorMessage = "O campo endereço de carteira é obrigatório")]
    [RegularExpression("^0x[a-fA-F0-9]{40}$", ErrorMessage = "O  endereço de carteira deve estar em formato correto")]
    [Display(Name = "Endereço de Carteira", Prompt = "0x71C7656EC7ab88b098defB751B7401B5f6d89553"), StringLength(42)]
    [JsonPropertyName("address")]
    public string Carteira { get; set; } = null!;

    [Required(ErrorMessage = "O campo início de jornada é obrigatório")]
    [Display(Name = "Início de Jornada", Prompt = "00:00")]
    [JsonPropertyName("begginingWorkDay")]
    public TimeOnly InicioJornada { get; set; }

    [Required(ErrorMessage = "O campo fim de jornada é obrigatório")]
    [Display(Name = "Fim de Jornada", Prompt = "00:00")]
    [JsonPropertyName("endWorkDay")]
    public TimeOnly FimJornada { get; set; }

    [Required(ErrorMessage = "O campo carteira do Empregador é obrigatório")]
    [RegularExpression("^0x[a-fA-F0-9]{40}$", ErrorMessage = "O  endereço de carteira deve estar em formato correto")]
    [Display(Name = "Carteira Empregador", Prompt = "0x71C7656EC7ab88b098defB751B7401B5f6d89553"), StringLength(42)]
    [JsonPropertyName("employerAddress")]
    public string Empregador { get; set; } = null!;
    
    [Range(0, 1, ErrorMessage = "O campo estado deve ser 0 ou 1")]
    [JsonPropertyName("state")]
    public int Ativo { get; set; }
}
