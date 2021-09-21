using System;

namespace EnumerationValues.Interface
{
    public interface IEnumValues<TEnum>
        where TEnum : Enum
    {
        TEnum[] Values { get; }
    }
}
