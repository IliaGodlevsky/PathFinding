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

        public EnumValuesWithoutIgnored(IEnumValues<TEnum> enumValues)
        {
            valuesWithoutIgnored = new Lazy<TEnum[]>(enumValues.Values
                .Where(value => !value.IsIgnored()).ToArray);
        }

        private readonly Lazy<TEnum[]> valuesWithoutIgnored;
    }
}
