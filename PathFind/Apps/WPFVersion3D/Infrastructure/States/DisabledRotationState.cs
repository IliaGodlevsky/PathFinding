using WPFVersion3D.Messages.PassValueMessages;
using WPFVersion3D.ViewModel;

namespace WPFVersion3D.Infrastructure.States
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
