using ConsoleVersion.Interface;
using System;
using ValueRange;

namespace ConsoleVersion.ValueInput
{
    internal abstract class ConsoleValueInput<TValue> : IValueInput<TValue>
        where TValue : struct, IComparable
    {
        public virtual TValue InputValue(string accompanyingMessage,
            TValue upperRangeValue, TValue lowerRangeValue = default)
        {
            var rangeOfValidInput = new InclusiveValueRange<TValue>(upperRangeValue, lowerRangeValue);
            string userInput;
            do
            {
                Console.Write(accompanyingMessage);
                userInput = Console.ReadLine();
            } while (!IsValidInput(userInput, rangeOfValidInput));

            return Parse(userInput);
        }

        protected abstract bool IsValidInput(string input,
            InclusiveValueRange<TValue> valueRange);

        protected abstract TValue Parse(string input);
    }
}
