using WPFVersion3D.Messages.PassValueMessages;
using WPFVersion3D.ViewModel;

namespace WPFVersion3D.Infrastructure.States
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
