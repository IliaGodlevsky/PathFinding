using ConsoleVersion.Interface;
using ValueRange;

namespace ConsoleVersion.ValueInput
{
    internal sealed class Int32ConsoleValueInput : ConsoleValueInput<int>, IValueInput<int>
    {
        protected override bool IsValidInput(string userInput,
            InclusiveValueRange<int> valueRange)
        {
            return int.TryParse(userInput, out var input)
                && valueRange.Contains(input);
        }

        protected override int Parse(string userInput)
        {
            return int.Parse(userInput);
        }
    }
}