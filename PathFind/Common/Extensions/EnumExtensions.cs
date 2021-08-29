using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class EnumExtensions
    {
        public static TAttribute GetAttributeOrNull<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            return value
                .GetType()
                .GetField(value.ToString())
                ?.GetAttributeOrNull<TAttribute>();
        }

        public static IDictionary<TKey, TEnum> ToDictionary<TKey, TEnum>(Func<TEnum, TKey> keySelector)
            where TEnum : Enum
        {
            return Enum
                .GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .ToDictionary(keySelector);
        }

        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue, TEnum>(
            Func<TEnum, TKey> keySelector,
            Func<TEnum, TValue> valueSelector)
            where TEnum : Enum
        {
            return Enum
                .GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .ToDictionary(keySelector, valueSelector);
        }

        public static IDictionary<TKey, TEnum> ToOrderedDictionary<TKey, TEnum>(
            Func<TEnum, TKey> keySelector, Func<KeyValuePair<TKey, TEnum>, TKey> orderSelector)
            where TEnum : Enum
        {
            return ToDictionary(keySelector).OrderBy(orderSelector).AsDictionary();
        }
    }
}
