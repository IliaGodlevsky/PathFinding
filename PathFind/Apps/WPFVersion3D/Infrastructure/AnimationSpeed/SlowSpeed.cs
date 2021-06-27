using AssembleClassesLib.Attributes;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Infrastructure.AnimationSpeed
{
    [ClassOrder(1)]
    [ClassName("Slow")]
    internal sealed class SlowSpeed : IAnimationSpeed
    {
        public double Speed => 2400;
    }
}
