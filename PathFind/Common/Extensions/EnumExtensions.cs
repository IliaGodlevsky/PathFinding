using System;
using System.ComponentModel;

namespace Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumValue)
        {
            var enumValueName = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValueName);
            var attribute = fieldInfo.GetAttribute<DescriptionAttribute>();
          
            return attribute?.Description ?? enumValueName;
        }

        public static TResultType GetValue<TResultType>(this Enum enumValue)
            where TResultType : struct, IConvertible
        {
            var value = Enum.Parse(enumValue.GetType(), enumValue.ToString());
            return (TResultType)Convert.ChangeType(value, typeof(TResultType));
        }
    }
}
