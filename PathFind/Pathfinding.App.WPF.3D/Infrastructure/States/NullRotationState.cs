using Pathfinding.App.WPF._3D.Interface;
using Pathfinding.App.WPF._3D.ViewModel;
using Shared.Primitives.Single;

namespace Pathfinding.App.WPF._3D.Infrastructure.States
{
    internal sealed class NullRotationState : Singleton<NullRotationState, IRotationState>, IRotationState
    {
        public bool CanRotate => false;

        public void Activate(GraphFieldAxisRotatingViewModel model)
        {

        }

        private NullRotationState()
        {

        }
    }
}
