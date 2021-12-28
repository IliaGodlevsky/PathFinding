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
        public TEnum[] Values => valuesWithoutIgnored.Value;

        public EnumValuesWithoutIgnored()
        {
            valuesWithoutIgnored = new Lazy<TEnum[]>(GetNotIgnoredValues);
        }

        private IReadOnlyCollection<TEnum> GetIgnored()
        {
            return typeof(TEnum)
                .GetCustomAttributes(false)
                .OfType<EnumValuesIgnoreAttribute>()
                .SelectMany(attribute => attribute.Ignored)
                .Distinct()
                .Cast<TEnum>()
                .ToArray();
        }

        private IReadOnlyCollection<TEnum> GetAllValues()
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToArray();
        }

        private TEnum[] GetNotIgnoredValues()
        {
            return GetAllValues().Except(GetIgnored()).ToArray();
        }

        private readonly Lazy<TEnum[]> valuesWithoutIgnored;
    }
}