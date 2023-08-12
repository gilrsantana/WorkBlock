namespace WorkBlockApp.Util;

public class FormatCNPJ
{
    public static string Format(string cnpj)
    {
        if (string.IsNullOrEmpty(cnpj)) return "";
        var cnpjFormatado = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
        if (cnpj.Length != 14)
        {
            cnpj = cnpj.PadLeft(14, '0');
        }
        return Convert.ToUInt64(cnpjFormatado).ToString(@"00\.000\.000\/0000\-00");
    }
}
