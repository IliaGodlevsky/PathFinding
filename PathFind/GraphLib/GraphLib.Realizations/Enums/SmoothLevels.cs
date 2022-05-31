using EnumerationValues.Attributes;
using GraphLib.Realizations.SmoothLevel;
using System.ComponentModel;

namespace GraphLib.Realizations.Enums
{
    [EnumValuesIgnore(None)]
    public enum SmoothLevels
    {
        None = 0,

        [Description("Low")]
        [SmoothLevel(1)]
        Low = 1,

        [Description("Medium")]
        [SmoothLevel(2)]
        Medium = 2,

        [Description("High")]
        [SmoothLevel(3)]
        High = 3,

        [Description("Flat")]
        [SmoothLevel(10)]
        Flat = 4
    }
}