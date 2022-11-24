using Pathfinding.App.WPF._3D.Interface;
using Shared.Primitives.Single;
using System.Windows.Media.Media3D;

namespace Pathfinding.App.WPF._3D.Infrastructure.Animators
{
    internal sealed class NullAnimatedAxisRotator : Singleton<NullAnimatedAxisRotator, IAnimatedAxisRotator>, IAnimatedAxisRotator
    {
        private NullAnimatedAxisRotator()
        {

        }

        public void RotateAxis(AxisAngleRotation3D angleRotation)
        {

        }
    }
}