using System.Windows.Media.Media3D;

namespace WPFVersion3D.Interface
{
    internal interface IAnimatedAxisRotatorFactory
    {
        IAnimatedAxisRotator CreateRotator(AxisAngleRotation3D axis, IAnimationSpeed speed);
    }
}
