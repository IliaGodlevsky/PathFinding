using EnumerationValues.Realizations;
using System;
using System.Linq;
using ValueRange;
using ValueRange.Extensions;

namespace ConsoleVersion.ValueInput
{
    internal sealed class EnumConsoleValueInput<TEnum> : ConsoleValueInput<TEnum>
        where TEnum : struct, Enum
    {
        public EnumConsoleValueInput()
        {
            enumValues = new EnumValues<TEnum>();
        }

        protected override bool IsValidInput(string userInput,
            InclusiveValueRange<TEnum> valueRange, out TEnum result)
        {
            return Enum.TryParse(userInput, ignoreCase: true, out result)
                && enumValues.Values.Contains(result)
                && valueRange.Contains(result);
        }

        private readonly EnumValues<TEnum> enumValues;
    }
}
