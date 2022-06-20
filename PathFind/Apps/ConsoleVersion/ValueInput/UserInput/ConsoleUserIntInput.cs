﻿namespace ConsoleVersion.ValueInput.UserInput
{
    internal sealed class ConsoleUserIntInput : ConsoleUserInput<int, int>
    {
        protected override int Convert(int inner)
        {
            return inner;
        }

        protected override bool IsValidInput(string userInput, out int result)
        {
            return int.TryParse(userInput, out result);
        }
    }
}