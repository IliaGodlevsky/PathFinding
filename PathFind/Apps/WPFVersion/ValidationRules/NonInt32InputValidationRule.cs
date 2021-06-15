using System.Globalization;
using System.Windows.Controls;

namespace WPFVersion.ValidationRules
{
    internal sealed class NonInt32InputValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (int.TryParse(value?.ToString(), out _))
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, null);
        }
    }
}
