using ValueRange;
using ValueRange.Extensions;

namespace ConsoleVersion.ValueInput
{
    internal sealed class IntConsoleValueInput : ConsoleValueInput<int>
    {
        protected override bool IsValidInput(string userInput, InclusiveValueRange<int> valueRange, out int result)
        {
            return int.TryParse(userInput, out result) && valueRange.Contains(result);
        }
    }
}