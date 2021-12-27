using EnumerationValues.Attributes;
using System;

namespace WPFVersion.Enums
{
    [Flags]
    internal enum MessageTokens
    {
        [EnumValuesIgnore]
        None = 0,
        MainModel = 2 << 0,
        AlgorithmStatisticsModel = 2 << 1,
        PathfindingModel = 2 << 2,
        VisualizationModel = 2 << 3,
        [EnumValuesIgnore]
        Everyone = (2 << 4) - 1
    }
}
