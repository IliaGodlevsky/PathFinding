using Pathfinding.App.WPF._3D.ViewModel;

namespace Pathfinding.App.WPF._3D.Interface
{
    internal interface IRotationState
    {
        void Activate(GraphFieldAxisRotatingViewModel model);

        bool CanRotate { get; }
    }
}
