namespace Pathfinding.App.Console.ValueInput.UserInput
{
    internal sealed class ConsoleUserIntInput : ConsoleUserInput<int, int>
    {
        protected override int Interpret(int inner)
        {
            return inner;
        }

        protected override bool TryParse(string userInput, out int result)
        {
            return int.TryParse(userInput, out result);
        }
    }
}