using System.ComponentModel.DataAnnotations;
using WorkBlockApi.Models.ValueObjects;

namespace WorkBlockApi.ViewModels;

public class EmployeeViewModel
{
    [Required(ErrorMessage = "Address is required")]
    [MinLength(10, ErrorMessage = "Address min length 10")]
    public string Address { get; set; } = null!;

    public string Name { get; set; } = null!;

    public ulong TaxId { get; set; }

    public uint BegginingWorkDay { get; set; }

    public uint EndWorkDay { get; set; }

    public string EmployerAddress { get; set; } = null!;

    [Range(0, 1, ErrorMessage = "State must be 0 or 1")]
    public byte State { get; set; }
}