using System;

namespace Pathfinding.App.Console.ValueInput.UserInput
{
    internal sealed class ConsoleUserTimeSpanInput : ConsoleUserInput<TimeSpan, double>
    {
        protected override TimeSpan Convert(double inner)
        {
            return TimeSpan.FromMilliseconds(inner);
        }

        protected override bool IsValidInput(string input, out double parsed)
        {
            return double.TryParse(input, out parsed);
        }
    }
}
