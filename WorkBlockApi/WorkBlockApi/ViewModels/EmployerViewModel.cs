using System.ComponentModel.DataAnnotations;
using WorkBlockApi.Models.ValueObjects;

namespace WorkBlockApi.ViewModels;

public class EmployerViewModel
{
    [Required(ErrorMessage = "Address is required")]
    [MinLength(10, ErrorMessage = "Address min length 10")]
    public string Address { get; set; }

    public string Name { get; set; }

    public ulong TaxId { get; set; }

    public Address LegalAddress { get; set; }
}