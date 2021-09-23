using Common.Extensions;
using ConsoleVersion.ValueInput.Interface;
using System;

namespace ConsoleVersion.ValueInput
{
    internal sealed class EnumValueInput<TEnum> : IValueInput<TEnum>
        where TEnum : struct, Enum
    {
        public static EnumValueInput<TEnum> Instance => instance.Value;

        public TEnum InputValue(string accompanyingMessage, TEnum upperRangeValue, 
            TEnum lowerRangeValue = default)
        {
            string userInput;
            do
            {
                Console.Write(accompanyingMessage);
                userInput = Console.ReadLine();
            } while (!IsValidEnumInput(userInput, upperRangeValue, lowerRangeValue));

            return (TEnum)Enum.Parse(type, userInput, ignoreCase);
        }

        private static bool IsValidEnumInput(string userInput, 
            TEnum upperValueRange, TEnum lowerValueRange)
        {
            return Enum.TryParse(userInput, ignoreCase, out TEnum input)
                && Enum.IsDefined(type, input)
                && input.IsBetween(upperValueRange, lowerValueRange);
        }

        private EnumValueInput()
        {

        }

        static EnumValueInput()
        {
            instance = new Lazy<EnumValueInput<TEnum>>(() => new EnumValueInput<TEnum>());
            ignoreCase = true;
            type = typeof(TEnum);
        }

        private static readonly Type type;
        private static readonly bool ignoreCase;
        private static readonly Lazy<EnumValueInput<TEnum>> instance;
    }
}
