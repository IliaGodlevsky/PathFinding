using System;

namespace ConsoleVersion.ValueInput.UserInput
{
    internal sealed class ConsoleUserEnumInput<T> : ConsoleUserInput<T>
        where T : struct, Enum
    {
        protected override bool IsValidInput(string userInput, out T result)
        {
            return Enum.TryParse(userInput, ignoreCase: true, out result)
                && Enum.IsDefined(typeof(T), result);
        }
    }
}
