using AssembleClassesLib.Attributes;

namespace WPFVersion3D.Interface
{
    /// <summary>
    /// An interface that responds for 
    /// graph field rotation speed
    /// </summary>
    [NotLoadable]
    internal interface IAnimationSpeed
    {
        double Milliseconds { get; }
    }
}
