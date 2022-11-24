using Pathfinding.App.WPF._3D.Infrastructure.Animators;
using Pathfinding.App.WPF._3D.Interface;

namespace Pathfinding.App.WPF._3D.Model.RotatorFactories
{
    internal sealed class BackwardAnimatedAxisRotatorFactory : IAnimatedAxisRotatorFactory
    {
        public IAnimatedAxisRotator Create(IAnimationSpeed speed)
        {
            return new BackwardAnimatedAxisRotator(speed);
        }
    }
}
