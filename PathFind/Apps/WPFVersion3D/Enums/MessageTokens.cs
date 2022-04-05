using EnumerationValues.Attributes;
using System;

namespace WPFVersion3D.Enums
{
    [Flags]
    [EnumValuesIgnore(None, Everyone)]
    internal enum MessageTokens : ulong
    {
        None = 0,

        MainModel = 2 << 0,
        AlgorithmStatisticsModel = 2 << 1,
        PathfindingModel = 2 << 2,
        StretchAlongAxisModel = 2 << 3,
        GraphFieldModel = 2 << 4,
        SaveGraphViewModel = 2 << 5,
        ClearColorsModel = 2 << 6,
        LoadGraphModel = 2 << 7,
        InterruptAllAlgorithmsModel = 2 << 8,
        CreateGraphModel = 2 << 9,
        ClearGraphModel = 2 << 10,
        ChangeOpacityModel = 2 << 11,

        Everyone = (2 << 12) - 1
    }
}
