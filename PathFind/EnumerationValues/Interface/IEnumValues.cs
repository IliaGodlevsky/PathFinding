using System;
using System.Collections.Generic;

namespace EnumerationValues.Interface
{
    public interface IEnumValues<TEnum>
        where TEnum : Enum
    {
        IReadOnlyCollection<TEnum> Values { get; }
    }
}
