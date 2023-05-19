using System.ComponentModel.DataAnnotations;

namespace WorkBlockApi.ViewModels;

public class AdministratorViewModel
{

    [Required(ErrorMessage = "Address is required")]
    [MinLength(10, ErrorMessage = "Address min length 10")]
    public string Address { get; set; } = null!;
    
    public string Name { get; set; } = null!;
    
    public ulong TaxId { get; set; }

    [Range(0, 1, ErrorMessage = "State must be 0 or 1")]
    public byte State { get; set; }
}
