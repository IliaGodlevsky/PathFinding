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
        Everyone = (2 << 3) - 1
    }
}
