using System.Text.RegularExpressions;

namespace WorkBlockApp.Models.Utils;

public class DocumentModel
{
    private static readonly Regex SWhitespace = new Regex(@"\s+");
    public static string ChangeWhiteSpace(string input, string replacement)
    {
        return SWhitespace.Replace(input, replacement);
    }
}