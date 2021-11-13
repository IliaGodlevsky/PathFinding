using ConsoleVersion.Interface;
using EnumerationValues.Realizations;
using System;
using System.Linq;
using ValueRange;

namespace ConsoleVersion.ValueInput
{
    internal sealed class EnumConsoleValueInput<TEnum> : ConsoleValueInput<TEnum>, IValueInput<TEnum>
        where TEnum : struct, Enum
    {
        public EnumConsoleValueInput()
        {
            ignoreCase = true;
            type = typeof(TEnum);
            enumValues = new EnumValues<TEnum>();
        }

        protected override bool IsValidInput(string userInput,
            InclusiveValueRange<TEnum> valueRange)
        {
            return Enum.TryParse(userInput, ignoreCase, out TEnum input)
                && enumValues.Values.Contains(input)
                && valueRange.Contains(input);
        }

        protected override TEnum Parse(string input)
        {
            return (TEnum)Enum.Parse(type, input, ignoreCase);
        }

        private readonly EnumValues<TEnum> enumValues;
        private readonly Type type;
        private readonly bool ignoreCase;
    }
}
