using WorkBlockApp.Models.Utils;
using WorkBlockApp.Models.ValueObjects.Documents;

namespace WorkBlockApp.Models.Documents;

public class CpfModel : DocumentModel
{
    public string CpfNumber { get; set; }
    public CpfModel(string cpfNumber)
    {
        CpfNumber = cpfNumber ?? throw new ArgumentNullException(nameof(cpfNumber));
    }

    public override bool IsValidDocument()
    {
        return ValidateCpf(CpfNumber);
    }
    
    private static bool ValidateCpf(string cpf)
    {
        if (string.IsNullOrEmpty(cpf))
                return false;

        var multiplicador1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        var multiplicador2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        var invalids = new[]
        {
            "00000000000", "11111111111", "22222222222", "33333333333",
            "44444444444", "55555555555", "66666666666", "77777777777", "88888888888", "99999999999"
        };
        cpf = ChangeWhiteSpace(cpf, "");
        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");
        if (cpf.Length != 11)
            return false;

        if (invalids.Any(i => cpf.Equals(i)))
            return false;

        var tempCpf = cpf.Substring(0, 9);
        var soma = 0;
        for (var i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
        var resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        var digito = resto.ToString();
        tempCpf += digito;
        soma = 0;
        for (var i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito += resto.ToString();
        return cpf.EndsWith(digito);
    }
}