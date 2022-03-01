using System.Windows.Media.Media3D;
using WPFVersion3D.Infrastructure.Animators;
using WPFVersion3D.Interface;

namespace WPFVersion3D.RotatorsFactories
{
    internal sealed class ForwardAnimatedAxisRotatorFactory : IAnimatedAxisRotatorFactory
    {
        public IAnimatedAxisRotator CreateRotator(AxisAngleRotation3D axis, IAnimationSpeed speed)
        {
            return new ForwardAnimatedAxisRotator(axis, speed);
        }
    }
}