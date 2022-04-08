using WPFVersion3D.Infrastructure.Animators;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Model.RotatorFactories
{
    internal sealed class ForwardAnimatedAxisRotatorFactory : IAnimatedAxisRotatorFactory
    {
        public IAnimatedAxisRotator Create(IAnimationSpeed speed)
        {
            return new ForwardAnimatedAxisRotator(speed);
        }
    }
}