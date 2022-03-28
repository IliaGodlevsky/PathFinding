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
        VertexStateModel = 2 << 2,
        EndPointsViewModel = 2 << 3,
        Everyone = (2 << 4) - 1
    }
}
