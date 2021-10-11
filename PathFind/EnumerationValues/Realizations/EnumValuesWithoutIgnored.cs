using EnumerationValues.Extensions;
using EnumerationValues.Interface;
using System;
using System.Linq;

namespace EnumerationValues.Realizations
{
    public sealed class EnumValuesWithoutIgnored<TEnum> : IEnumValues<TEnum>
        where TEnum : Enum
    {
        public TEnum[] Values => valuesWithoutIgnored.Value;

        public EnumValuesWithoutIgnored()
        {
            valuesWithoutIgnored = new Lazy<TEnum[]>(new EnumValues<TEnum>()
                .Values
                .Where(value => !value.IsIgnored())
                .ToArray);
        }

        private readonly Lazy<TEnum[]> valuesWithoutIgnored;
    }
}
