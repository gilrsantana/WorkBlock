using System.ComponentModel.DataAnnotations;
using WorkBlockApp.Models.ValueObjects.Documents;

namespace WorkBlockApp.Models.DataAnnotation;

public class RequiredValidPis : ValidationAttribute
{
    /// <summary>
    /// Designed for text to ensure that a selection is valid and not the dummy "SELECT" entry
    /// </summary>
    /// <param name="value">The string value of the field</param>
    /// <returns>True if value is a valid PIS</returns>
    public override bool IsValid(object? value)
    {
        // return true if value is a valid PIS, otherwise return false
        if (value == null)
            return false;
        return new PisModel(value.ToString()!).IsValidDocument();
    }
}
