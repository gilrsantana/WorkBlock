namespace WorkBlockApp.DTOs;

public class AdministratorResponse
{
    public string Nome { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public string Carteira { get; set; } = null!;
    public int Ativo { get; set; }
}