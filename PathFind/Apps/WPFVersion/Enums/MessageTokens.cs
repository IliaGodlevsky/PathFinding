using EnumerationValues.Attributes;
using System;

namespace WPFVersion.Enums
{
    [Flags]
    [EnumValuesIgnore(MessageTokens.None, MessageTokens.Everyone)]
    internal enum MessageTokens
    {
        None = 0,
        MainModel = 2 << 0,
        AlgorithmStatisticsModel = 2 << 1,
        PathfindingModel = 2 << 2,
        VisualizationModel = 2 << 3,
        Everyone = (2 << 4) - 1
    }
}
