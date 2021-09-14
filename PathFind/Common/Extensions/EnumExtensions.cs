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

        public static IDictionary<string, TEnum> ToDescriptionAdjustedOrderedDictionary<TEnum>()
            where TEnum : Enum
        {
            var values = Enum.GetValues(typeof(TEnum)).OfType<TEnum>();
            var descriptions = values.Select(@enum => @enum.GetDescriptionAttributeValueOrTypeName());
            int longestDescriptionLength = descriptions.Max(description => description.Length);

            string PadRight(string description) => description.PadRight(longestDescriptionLength);
            KeyValuePair<string, TEnum> ToKeyValuePair(string key, TEnum value)
                => new KeyValuePair<string, TEnum>(key, value);

            return descriptions
                .Select(PadRight)
                .Zip(values, ToKeyValuePair)
                .OrderBy(item => item.Key)
                .ToDictionary();
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
    }
}
