using ConsoleVersion.Model;
using System;

namespace ConsoleVersion.ValueInput.UserInput
{
    internal sealed class ConsoleUserAnswerInput : ConsoleUserInput<Answer>
    {
        protected override bool IsValidInput(string userInput, out Answer result)
        {
            result = (Answer)userInput;
            return result != Answer.None;
        }
    }
}
