using WorkBlockApp.Models.ValueObjects;

namespace WorkBlockApp.DTOs;

public class EmployerResponse
{
    public string Nome { get; set; } = null!;
    public string Cnpj { get; set; } = null!;
    public string Carteira { get; set; } = null!;
    public AddressModel Endereco { get; set; } = null!;
}
