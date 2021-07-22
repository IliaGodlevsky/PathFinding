using System.ComponentModel;
using WPFVersion3D.Attributes;

namespace WPFVersion3D.Enums
{
    internal enum AnimationSpeed
    {
        [Description("Slowest")]
        [Speed(milliseconds: 4800)]
        Slowest,

        [Description("Slow")]
        [Speed(milliseconds: 2400)]
        Slow,

        [Description("Medium")]
        [Speed(milliseconds: 1200)]
        Medium,

        [Description("High")]
        [Speed(milliseconds: 600)]
        High,

        [Description("Highest")]
        [Speed(milliseconds: 300)]
        Highest,

        [Description("Random")]
        [RandomSpeed(300, 4800)]
        Random
    }
}