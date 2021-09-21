using System;
using System.Linq;

namespace Common
{
    public sealed class EnumValues<TEnum>
        where TEnum : Enum
    {
        public TEnum[] Values => values.Value;

        public EnumValues()
        {
            enumType = typeof(TEnum);
            values = new Lazy<TEnum[]>(Enum.GetValues(enumType).Cast<TEnum>().ToArray);
        }

        private readonly Lazy<TEnum[]> values;
        private readonly Type enumType;
    }
}
