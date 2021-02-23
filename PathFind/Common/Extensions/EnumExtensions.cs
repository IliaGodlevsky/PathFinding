using System;

namespace Common.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns value of <paramref name="enumValue"/> 
        /// casted to <typeparamref name="TResultType"/>
        /// </summary>
        /// <typeparam name="TResultType"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns>A value of <paramref name="enumValue"/> 
        /// casted to <typeparamref name="TResultType"/></returns>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static TResultType Parse<TResultType>(this Enum enumValue)
            where TResultType : struct, IConvertible
        {
            var value = Enum.Parse(enumValue.GetType(), enumValue.ToString());
            return (TResultType)Convert.ChangeType(value, typeof(TResultType));
        }
    }
}
