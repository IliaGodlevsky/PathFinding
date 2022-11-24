using Pathfinding.App.WPF._3D.Messages.PassValueMessages;
using Pathfinding.App.WPF._3D.ViewModel;

namespace Pathfinding.App.WPF._3D.Infrastructure.States
{
    internal sealed class DisabledRotationState : RotationState
    {
        public override bool CanRotate => false;

        public override void Activate(GraphFieldAxisRotatingViewModel model)
        {
            messenger.Send(new AddRotationViewModelMessage(model));
        }
    }
}
