using AssembleClassesLib.Attributes;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Infrastructure.AnimationSpeed
{
    [ClassOrder(4)]
    [ClassName("Very high")]
    internal sealed class VeryHighSpeed : IAnimationSpeed
    {
        public double Speed => 300;
    }
}
