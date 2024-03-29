﻿using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console.ValueInput.UserInput
{
    internal sealed class ConsoleUserAnswerInput : ConsoleUserInput<Answer, Answer>
    {
        protected override Answer Interpret(Answer inner)
        {
            return inner;
        }

        protected override bool TryParse(string userInput, out Answer result)
        {
            return Answer.TryParse(userInput, out result);
        }
    }
}
