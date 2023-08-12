namespace WorkBlockApp.Util;

public static class FormatDate
{
    public static string Format(ulong value)
    {
        var date = value.ToString();
        if (string.IsNullOrEmpty(date)) return "";
        return date.Substring(6, 2) + "/" + date.Substring(4, 2) + "/" + date.Substring(0, 4);
    }
}
