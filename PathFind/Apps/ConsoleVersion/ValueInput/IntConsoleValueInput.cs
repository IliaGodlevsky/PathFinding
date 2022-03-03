using ValueRange;
using ValueRange.Extensions;

namespace ConsoleVersion.ValueInput
{
    internal sealed class IntConsoleValueInput : ConsoleValueInput<int>
    {
        protected override bool IsValidInput(string userInput, InclusiveValueRange<int> valueRange)
        {
            return int.TryParse(userInput, out var input) && valueRange.Contains(input);
        }

        protected override int Parse(string userInput)
        {
            return int.Parse(userInput);
        }
    }
}