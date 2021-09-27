using EnumerationValues.Attributes;
using System.ComponentModel;
using WPFVersion3D.Attributes;

namespace WPFVersion3D.Enums
{
    internal enum AnimationSpeeds
    {
        [EnumValuesIgnore]
        None = 0,

        [Description("Slowest")]
        [AnimationSpeed(milliseconds: 4800)]
        Slowest = 1,

        [Description("Slow")]
        [AnimationSpeed(milliseconds: 2400)]
        Slow = 2,

        [Description("Medium")]
        [AnimationSpeed(milliseconds: 1200)]
        Medium = 3,

        [Description("High")]
        [AnimationSpeed(milliseconds: 600)]
        High = 4,

        [Description("Highest")]
        [AnimationSpeed(milliseconds: 300)]
        Highest = 5,

        [Description("Random")]
        [RandomAnimationSpeed(300, 4800)]
        Random = 6
    }
}