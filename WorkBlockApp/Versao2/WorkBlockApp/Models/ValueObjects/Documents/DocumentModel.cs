using System.Text.RegularExpressions;
using WorkBlockApp.Interfaces.IModels.IValueObjects.IDocuments;

namespace WorkBlockApp.Models.ValueObjects.Documents;

public abstract class DocumentModel : IDocument
{
    private static readonly Regex SWhitespace = new Regex(@"\s+");
    protected static string ChangeWhiteSpace(string input, string replacement)
    {
        return SWhitespace.Replace(input, replacement);
    }
    
    public virtual bool IsValidDocument()
    {
        return false;
    }
}