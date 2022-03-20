using EnumerationValues.Attributes;
using System;

namespace EnumerationValues.Tests
{
    [Flags]
    [EnumValuesIgnore(None, All)]
    internal enum TestEnum
    {
        None = 0,
        One = 2 << 1,
        Two = 2 << 2,
        Three = 2 << 3,
        Four = 2 << 4,
        Five = 2 << 5,
        Six = 2 << 6,
        Seven = 2 << 7,
        All = (2 << 8) - 1
    }
}
