using Common.ValueRanges;
using ConsoleVersion.Interface;
using System;

namespace ConsoleVersion.ValueInput
{
    internal sealed class Int32ValueInput : IValueInput<int>
    {
        /// <summary>
        /// Return user's console input in range of values
        /// </summary>
        /// <param name="accompanyingMessage">An input message</param>
        /// <param name="upperRangeValue">An upper value of input range</param>
        /// <param name="lowerRangeValue">A lower value of input range</param>
        /// <returns>A number in the range from 
        /// <paramref name="lowerRangeValue"/> to 
        /// <paramref name="upperRangeValue"/></returns>
        /// <exception cref="System.IO.IOException"></exception>
        public int InputValue(string accompanyingMessage,
            int upperRangeValue, int lowerRangeValue = default)
        {
            var rangeOfValidInput = new InclusiveValueRange<int>(upperRangeValue, lowerRangeValue);
            string userInput;
            do
            {
                Console.Write(accompanyingMessage);
                userInput = Console.ReadLine();
            } while (!IsValidInput(userInput, rangeOfValidInput));

            return Convert.ToInt32(userInput);
        }

        private bool IsValidInput(string userInput,
            InclusiveValueRange<int> rangeOfValidInput)
        {
            return int.TryParse(userInput, out var input)
                && rangeOfValidInput.Contains(input);
        }
    }
}