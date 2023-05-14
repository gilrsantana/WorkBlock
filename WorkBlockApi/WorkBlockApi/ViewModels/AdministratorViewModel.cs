using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace WorkBlockApi.ViewModels;

public class AdministratorViewModel
{
    [Required(ErrorMessage = "Address is required")]
    [MinLength(10, ErrorMessage = "Address min length 10")]
    public string Address { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    [MinLength(3, ErrorMessage = "Name min length 3")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "TaxId is required")]
    [Range(100, ulong.MaxValue, ErrorMessage = "TaxId Min digits 3")]
    public ulong TaxId { get; set; }
}
