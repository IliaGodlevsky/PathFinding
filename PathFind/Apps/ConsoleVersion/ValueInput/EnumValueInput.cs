using Common.Extensions;
using Common.ValueRanges;
using ConsoleVersion.Interface;
using System;

namespace ConsoleVersion.ValueInput
{
    internal sealed class EnumValueInput<TEnum> : IValueInput<TEnum>
        where TEnum : struct, Enum
    {
        public EnumValueInput()
        {
            ignoreCase = true;
            type = typeof(TEnum);
        }

        public TEnum InputValue(string accompanyingMessage,
            TEnum upperRangeValue, TEnum lowerRangeValue)
        {
            var rangeOfValidInput = new InclusiveValueRange<TEnum>(upperRangeValue, lowerRangeValue);
            string userInput;
            do
            {
                Console.Write(accompanyingMessage);
                userInput = Console.ReadLine();
            } while (!IsValidEnumInput(userInput, rangeOfValidInput));

            return (TEnum)Enum.Parse(type, userInput, ignoreCase);
        }

        private bool IsValidEnumInput(string userInput, 
            InclusiveValueRange<TEnum> rangeOfvalidInput)
        {
            return Enum.TryParse(userInput, ignoreCase, out TEnum input)
                && Enum.IsDefined(type, input)
                && rangeOfvalidInput.Contains(input);
        }



        private readonly Type type;
        private readonly bool ignoreCase;
    }
}
