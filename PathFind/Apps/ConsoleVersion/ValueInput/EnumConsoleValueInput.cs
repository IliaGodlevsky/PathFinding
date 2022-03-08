using EnumerationValues.Interface;
using EnumerationValues.Realizations;
using System;
using System.Linq;
using ValueRange;
using ValueRange.Extensions;

namespace ConsoleVersion.ValueInput
{
    internal sealed class EnumConsoleValueInput<T> : ConsoleValueInput<T>
        where T : struct, Enum
    {
        public EnumConsoleValueInput()
        {
            enumValues = new EnumValues<T>();
        }

        protected override bool IsValidInput(string userInput,
            InclusiveValueRange<T> valueRange, out T result)
        {
            return Enum.TryParse(userInput, ignoreCase: true, out result)
                && enumValues.Values.Contains(result)
                && valueRange.Contains(result);
        }

        private readonly IEnumValues<T> enumValues;
    }
}
