using ConsoleVersion.Interface;
using System;

namespace ConsoleVersion.ValueInput.UserInput
{
    internal abstract class ConsoleUserInput<TOutput, TInner> : IInput<TOutput>
    {
        public TOutput Input()
        {
            TInner result;
            string userInput = Console.ReadLine();
            while (!IsValidInput(userInput, out result))
            {
                Console.Write(MessagesTexts.BadInputMsg);
                userInput = Console.ReadLine();
            }
            return Convert(result);
        }

        protected abstract TOutput Convert(TInner inner);

        protected abstract bool IsValidInput(string input, out TInner parsed);
    }
}
