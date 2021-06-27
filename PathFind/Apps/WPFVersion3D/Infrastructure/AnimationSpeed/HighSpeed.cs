using AssembleClassesLib.Attributes;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Infrastructure.AnimationSpeed
{
    [ClassOrder(3)]
    [ClassName("High")]
    internal sealed class HighSpeed : IAnimationSpeed
    {
        public double Speed => 600;
    }
}
