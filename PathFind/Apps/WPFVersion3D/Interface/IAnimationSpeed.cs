using AssembleClassesLib.Attributes;

namespace WPFVersion3D.Interface
{
    [NotLoadable]
    internal interface IAnimationSpeed
    {
        double Milliseconds { get; }
    }
}
