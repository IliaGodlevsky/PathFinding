using EnumerationValues.Interface;
using EnumerationValues.Realizations;
using System;
using System.Linq;
using ValueRange.Extensions;

namespace ConsoleVersion.ValueInput.UserInput
{
    internal sealed class ConsoleUserEnumInput<T> : ConsoleUserInput<T>
        where T : struct, Enum
    {
        public ConsoleUserEnumInput()
        {
            enumValues = new EnumValues<T>();
        }

        protected override bool IsValidInput(string userInput, out T result)
        {
            return Enum.TryParse(userInput, ignoreCase: true, out result)
                && enumValues.Values.Contains(result);
        }

        private readonly IEnumValues<T> enumValues;
    }
}
