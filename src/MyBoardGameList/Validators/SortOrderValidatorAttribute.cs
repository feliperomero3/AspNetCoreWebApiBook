using System.ComponentModel.DataAnnotations;

namespace MyBoardGameList.Validators;

/// <summary>
/// Check the input string against the "ASC" and "DESC" values and returns a successful result
/// only if there is an exact match with one of them.
/// </summary>
public class SortOrderValidatorAttribute : ValidationAttribute
{
    private readonly string[] _allowedValues = new[] { "ASC", "DESC" };
    private const string _defaultErrorMessage = "Value must be one of the following: {0}.";

    public SortOrderValidatorAttribute() : base(_defaultErrorMessage) { }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var strValue = value as string;

        if (!string.IsNullOrEmpty(strValue) && _allowedValues.Contains(strValue))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(FormatErrorMessage(string.Join(",", _allowedValues)));
    }
}
