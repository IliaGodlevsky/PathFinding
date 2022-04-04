using EnumerationValues.Attributes;
using System;

namespace WPFVersion3D.Enums
{
    [Flags]
    [EnumValuesIgnore(None, Everyone)]
    internal enum MessageTokens
    {
        None = 0,

        MainModel = 2 << 0,
        AlgorithmStatisticsModel = 2 << 1,
        PathfindingModel = 2 << 2,
        StretchAlongAxisModel = 2 << 3,
        GraphFieldModel = 2 << 4,

        Everyone = (2 << 5) - 1
    }
}
