using Pathfinding.App.WPF._3D.Messages.PassValueMessages;
using Pathfinding.App.WPF._3D.ViewModel;

namespace Pathfinding.App.WPF._3D.Infrastructure.States
{
    internal sealed class EnabledRotationState : RotationState
    {
        public override bool CanRotate => true;

        public override void Activate(GraphFieldAxisRotatingViewModel model)
        {
            messenger.Send(new RemoveRotationViewModelMessage(model));
        }
    }
}
