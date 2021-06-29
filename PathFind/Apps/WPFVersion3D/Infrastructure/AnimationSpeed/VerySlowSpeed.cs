using AssembleClassesLib.Attributes;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Infrastructure.AnimationSpeed
{
    [ClassOrder(0)]
    [ClassName("Very slow")]
    internal sealed class VerySlowSpeed : IAnimationSpeed
    {
        public double Milliseconds => 4800;
    }
}
