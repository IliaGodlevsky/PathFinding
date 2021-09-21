using EnumerationValues.Interface;
using System;
using System.Linq;

namespace EnumerationValues.Realizations
{
    public sealed class EnumValues<TEnum> : IEnumValues<TEnum>
        where TEnum : Enum
    {
        public TEnum[] Values => values.Value;

        public EnumValues()
        {
            values = new Lazy<TEnum[]>(Enum
                .GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .ToArray);
        }

        private readonly Lazy<TEnum[]> values;
    }
}
