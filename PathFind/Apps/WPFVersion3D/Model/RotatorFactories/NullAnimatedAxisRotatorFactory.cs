using SingletonLib;
using WPFVersion3D.Infrastructure.Animators;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Model.RotatorFactories
{
    internal sealed class NullAnimatedAxisRotatorFactory 
        : Singleton<NullAnimatedAxisRotatorFactory, IAnimatedAxisRotatorFactory>, IAnimatedAxisRotatorFactory
    {
        public IAnimatedAxisRotator Create(IAnimationSpeed speed) => NullAnimatedAxisRotator.Instance;
    }
}
