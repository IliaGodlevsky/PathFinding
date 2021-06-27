using AssembleClassesLib.Attributes;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Infrastructure.AnimationSpeed
{
    [ClassOrder(2)]
    [ClassName("Medium")]
    internal sealed class MediumSpeed : IAnimationSpeed
    {
        public double Speed => 1200;
    }
}
