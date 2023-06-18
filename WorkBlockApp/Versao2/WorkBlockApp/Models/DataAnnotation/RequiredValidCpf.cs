using System.ComponentModel.DataAnnotations;
using WorkBlockApp.Models.ValueObjects.Documents;

namespace WorkBlockApp.Models.DataAnnotation;

public class RequiredValidCpf : ValidationAttribute
{
    /// <summary>
    /// Designed for text to ensure that a selection is valid and not the dummy "SELECT" entry
    /// </summary>
    /// <param name="value">The string value of the field</param>
    /// <returns>True if value is a valid CPF</returns>
    public override bool IsValid(object? value)
    {
        // return true if value is a valid CPF, otherwise return false
        if (value == null)
            return false;
        return new CpfModel(value.ToString()!).IsValidDocument();
    }
}