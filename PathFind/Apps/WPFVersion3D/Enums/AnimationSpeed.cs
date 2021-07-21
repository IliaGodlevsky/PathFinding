using System.ComponentModel;
using WPFVersion3D.Attributes;

namespace WPFVersion3D.Enums
{
    internal enum AnimationSpeed
    {
        [Speed(milliseconds: 4800)]
        [Description("Slowest")]
        Slowest,
        [Speed(milliseconds: 2400)]
        [Description("Slow")]
        Slow,
        [Speed(milliseconds: 1200)]
        [Description("Medium")]
        Medium,
        [Speed(milliseconds: 600)]
        [Description("High")]
        High,
        [Speed(milliseconds: 300)]
        [Description("Highest")]
        Highest,
        [RandomSpeed(from: 300, to: 4800)]
        [Description("Random")]
        Random
    }
}