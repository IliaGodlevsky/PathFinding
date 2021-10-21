using EnumerationValues.Attributes;
using System.ComponentModel;
using WPFVersion3D.Attributes;

namespace WPFVersion3D.Enums
{
    /// <summary>
    /// Represents the speed of an animation
    /// </summary>
    internal enum AnimationSpeeds
    {
        [EnumValuesIgnore]
        None = 0,

        [Description("Extremely slow")]
        [AnimationSpeed(milliseconds: 9600)]
        ExtremelySlow,

        [Description("Slowest")]
        [AnimationSpeed(milliseconds: 4800)]
        Slowest,

        [Description("Slow")]
        [AnimationSpeed(milliseconds: 2400)]
        Slow,

        [Description("Medium")]
        [AnimationSpeed(milliseconds: 1200)]
        Medium,

        [Description("High")]
        [AnimationSpeed(milliseconds: 600)]
        High,

        [Description("Highest")]
        [AnimationSpeed(milliseconds: 300)]
        Highest,

        [Description("Random")]
        [RandomAnimationSpeed(300, 9600)]
        Random
    }
}