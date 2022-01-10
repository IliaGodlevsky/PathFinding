using EnumerationValues.Attributes;
using EnumerationValues.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnumerationValues.Realizations
{
    public sealed class EnumValuesWithoutIgnored<TEnum> : IEnumValues<TEnum>
        where TEnum : Enum
    {
        public IReadOnlyCollection<TEnum> Values => values.Value;

        public EnumValuesWithoutIgnored()
        {
            values = new Lazy<TEnum[]>(GetValues, isThreadSafe: true);
        }

        private TEnum[] GetValues()
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Except(typeof(TEnum)
                .GetCustomAttributes(false)
                .OfType<EnumValuesIgnoreAttribute>()
                .SelectMany(attribute => attribute.Ignored)
                .Distinct()
                .Cast<TEnum>())
                .ToArray();
        }

        private readonly Lazy<TEnum[]> values;
    }
}