using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkBlockApp.Models.Domain;

public class ReportModel
{
    [Required(ErrorMessage = "O campo endereço de carteira é obrigatório")]
    [RegularExpression("^0x[a-fA-F0-9]{40}$", ErrorMessage = "O  endereço de carteira deve estar em formato correto")]
    [Display(Name = "Endereço de Carteira", Prompt = "0x71C7...9553"), StringLength(42)]
    public string Carteira { get; set; } = null!;

    [Required(ErrorMessage = "O campo período é obrigatório")]
    [Display(Name = "Período de pesquisa"), StringLength(35)]
    public string Periodo { get; set; } = null!;
}
