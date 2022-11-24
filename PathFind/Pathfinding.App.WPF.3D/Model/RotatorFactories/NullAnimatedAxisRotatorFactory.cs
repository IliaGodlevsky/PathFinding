using Pathfinding.App.WPF._3D.Infrastructure.Animators;
using Pathfinding.App.WPF._3D.Interface;
using Shared.Primitives.Single;

namespace Pathfinding.App.WPF._3D.Model.RotatorFactories
{
    internal sealed class NullAnimatedAxisRotatorFactory
        : Singleton<NullAnimatedAxisRotatorFactory, IAnimatedAxisRotatorFactory>, IAnimatedAxisRotatorFactory
    {
        public IAnimatedAxisRotator Create(IAnimationSpeed speed) => NullAnimatedAxisRotator.Interface;
    }
}
