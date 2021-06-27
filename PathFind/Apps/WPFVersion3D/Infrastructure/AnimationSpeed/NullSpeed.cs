using AssembleClassesLib.Attributes;
using NullObject.Attributes;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Infrastructure.AnimationSpeed
{
    [Null]
    [NotLoadable]
    internal sealed class NullSpeed : IAnimationSpeed
    {
        public double Speed => 1;
    }
}
