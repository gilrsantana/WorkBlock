namespace WorkBlockApp.Util;

public static class FormatCEP
{
    public static string Format(string cep)
    {
        if (string.IsNullOrEmpty(cep)) return "";
        var cepFormatado = cep.Replace(".", "").Replace("-", "");
        if (cep.Length != 8)
        {
            cep = cep.PadLeft(8, '0');
        }
        return Convert.ToUInt64(cepFormatado).ToString(@"00000\-000");
    }
}
