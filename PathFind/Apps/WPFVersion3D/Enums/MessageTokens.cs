using EnumerationValues.Attributes;
using System;

namespace WPFVersion3D.Enums
{
    [Flags]
    internal enum MessageTokens
    {
        MainModel = 2 << 0,
        AlgorithmStatisticsModel = 2 << 1,
        PathfindingModel = 2 << 2,
        [EnumValuesIgnore]
        Everyone = (2 << 3) - 1

    }
}
