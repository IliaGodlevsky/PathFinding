using EnumerationValues.Attributes;
using System.ComponentModel;
using WPFVersion3D.Attributes;

namespace WPFVersion3D.Enums
{
    /// <summary>
    /// Represents the speed of an animation
    /// </summary>
    [EnumValuesIgnore(None)]
    internal enum AnimationSpeeds
    {
        None = 0,

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
        [RandomAnimationSpeed(300, 4800)]
        Random
    }
}