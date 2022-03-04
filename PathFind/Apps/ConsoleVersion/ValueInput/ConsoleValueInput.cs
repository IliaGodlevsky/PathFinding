using ConsoleVersion.Interface;
using System;
using ValueRange;

namespace ConsoleVersion.ValueInput
{
    internal abstract class ConsoleValueInput<T> : IValueInput<T> where T : struct, IComparable
    {
        public virtual T InputValue(string msg, T upper, T lower = default)
        {
            var range = new InclusiveValueRange<T>(upper, lower);
            string userInput;
            do
            {
                Console.Write(msg);
                userInput = Console.ReadLine();
            } while (!IsValidInput(userInput, range));

            return Parse(userInput);
        }

        protected abstract bool IsValidInput(string input, InclusiveValueRange<T> valueRange);

        protected abstract T Parse(string input);
    }
}
