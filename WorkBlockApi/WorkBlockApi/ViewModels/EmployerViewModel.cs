using System.ComponentModel.DataAnnotations;
using WorkBlockApi.Models.ValueObjects;

namespace WorkBlockApi.ViewModels;

public class EmployerViewModel
{
    [Required(ErrorMessage = "Address is required")]
    [MinLength(10, ErrorMessage = "Address min length 10")]
    public string Address { get; set; } = null!;

    public string Name { get; set; } = null!;

    public ulong TaxId { get; set; }

    [Required(ErrorMessage = "Legal Address is required")]
    public Address LegalAddress { get; set; } = null!;
}