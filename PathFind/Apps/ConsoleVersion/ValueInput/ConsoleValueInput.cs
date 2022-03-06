using ConsoleVersion.Interface;
using System;
using ValueRange;

namespace ConsoleVersion.ValueInput
{
    internal abstract class ConsoleValueInput<T> : IValueInput<T> where T : struct, IComparable
    {
        public T InputValue(string msg, T upper, T lower = default)
        {
            var range = new InclusiveValueRange<T>(upper, lower);
            T result;
            string userInput;
            do
            {
                Console.Write(msg);
                userInput = Console.ReadLine();
            } while (!IsValidInput(userInput, range, out result));

            return result;
        }

        protected abstract bool IsValidInput(string input, InclusiveValueRange<T> valueRange, out T parsed);
    }
}
