namespace WorkBlockApp.Util
{
    public static class FormatCPF
    {
        public static string Format(string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) return "";
            var cpfFormatado = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
            {
                cpf = cpf.PadLeft(11, '0');
            }
            return Convert.ToUInt64(cpfFormatado).ToString(@"000\.000\.000\-00");
        }
    }
}