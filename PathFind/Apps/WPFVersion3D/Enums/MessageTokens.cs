using EnumerationValues.Attributes;
using System;

namespace WPFVersion3D.Enums
{
    [Flags]
    [EnumValuesIgnore(None, Everyone, AllStretchModels)]
    internal enum MessageTokens
    {
        None = 0,
        MainModel = 2 << 0,
        AlgorithmStatisticsModel = 2 << 1,
        PathfindingModel = 2 << 2,
        StretchAlongXAxisModel = 2 << 3,
        StretchAlongYAxisModel = 2 << 4,
        StretchAlongZAxisModel = 2 << 5,
        AllStretchModels = StretchAlongXAxisModel | StretchAlongYAxisModel | StretchAlongZAxisModel,
        Everyone = (2 << 6) - 1
    }
}
