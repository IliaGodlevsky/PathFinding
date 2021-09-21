using EnumerationValues.Attributes;
using System.ComponentModel;
using WPFVersion3D.Attributes;

namespace WPFVersion3D.Enums
{
    internal enum AnimationSpeed
    {
        [EnumFetchIgnore]
        None = 0,

        [Description("Slowest")]
        [Speed(milliseconds: 4800)]
        Slowest = 1,

        [Description("Slow")]
        [Speed(milliseconds: 2400)]
        Slow = 2,

        [Description("Medium")]
        [Speed(milliseconds: 1200)]
        Medium = 3,

        [Description("High")]
        [Speed(milliseconds: 600)]
        High = 4,

        [Description("Highest")]
        [Speed(milliseconds: 300)]
        Highest = 5,

        [Description("Random")]
        [RandomSpeed(300, 4800)]
        Random = 6
    }
}