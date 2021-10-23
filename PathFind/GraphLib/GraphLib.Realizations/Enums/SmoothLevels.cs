using EnumerationValues.Attributes;
using GraphLib.Realizations.SmoothLevel;
using System.ComponentModel;

namespace GraphLib.Realizations.Enums
{
    public enum SmoothLevels
    {
        [EnumValuesIgnore]
        None = 0,

        [SmoothLevel(1)]
        [Description("Low")]        
        Low,

        [SmoothLevel(2)]
        [Description("Medium")]
        Medium,

        [SmoothLevel(3)]
        [Description("High")]
        High,

        [SmoothLevel(10)]
        [Description("Plain")]
        Plain
    }
}
