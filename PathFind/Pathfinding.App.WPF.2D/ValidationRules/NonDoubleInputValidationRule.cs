using System.Globalization;
using System.Windows.Controls;

namespace Pathfinding.App.WPF._2D.ValidationRules
{
    internal sealed class NonDoubleInputValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            value = value?.ToString().Replace('.', ',');
            if (double.TryParse(value?.ToString(), out _))
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, null);
        }
    }
}
