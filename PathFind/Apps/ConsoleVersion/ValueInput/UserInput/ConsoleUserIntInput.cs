namespace ConsoleVersion.ValueInput.UserInput
{
    internal sealed class ConsoleUserIntInput : ConsoleUserInput<int>
    {
        protected override bool IsValidInput(string userInput, out int result)
        {
            return int.TryParse(userInput, out result);
        }
    }
}