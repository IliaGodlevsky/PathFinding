using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System.Globalization;
using System.Windows.Controls;

namespace Pathfinding.App.WPF._3D.ValidationRules
{
    internal sealed class RangedIntValidationRule : ValidationRule
    {
        public InclusiveValueRange<int> ValueRange { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (IsValidValue(value))
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, null);
        }

        private bool IsValidValue(object value)
        {
            return int.TryParse(value?.ToString(), out var result)
                && ValueRange.Contains(result) == true;
        }
    }
}
