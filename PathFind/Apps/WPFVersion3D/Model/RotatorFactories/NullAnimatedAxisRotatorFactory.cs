using WPFVersion3D.Infrastructure.Animators;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Model.RotatorFactories
{
    internal sealed class NullAnimatedAxisRotatorFactory : IAnimatedAxisRotatorFactory
    {
        public IAnimatedAxisRotator Create(IAnimationSpeed speed)
        {
            return NullAnimatedAxisRotator.Instance;
        }
    }
}
