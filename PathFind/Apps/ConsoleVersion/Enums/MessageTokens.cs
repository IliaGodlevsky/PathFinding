using EnumerationValues.Attributes;
using System;

namespace ConsoleVersion.Enums
{
    [Flags]
    [EnumValuesIgnore(None, Everyone)]
    internal enum MessageTokens
    {
        None = 0,
        MainModel = 2 << 0,
        MainView = 2 << 1,
        EndPointsViewModel = 2 << 2,
        Everyone = (2 << 3) - 1
    }
}
