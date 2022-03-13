using ConsoleVersion.Interface;
using System;

namespace ConsoleVersion.ValueInput.UserInput
{
    internal abstract class ConsoleUserInput<T> : IInput<T> where T : struct, IComparable
    {
        public T Input()
        {
            T result;
            string userInput = Console.ReadLine();
            while (!IsValidInput(userInput, out result))
            {
                Console.Write(MessagesTexts.BadInputMsg);
                userInput = Console.ReadLine();
            }
            return result;
        }

        protected abstract bool IsValidInput(string input, out T parsed);
    }
}
