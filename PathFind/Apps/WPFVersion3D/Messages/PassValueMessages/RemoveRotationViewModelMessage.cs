using WPFVersion3D.ViewModel;

namespace WPFVersion3D.Messages.PassValueMessages
{
    internal sealed class RemoveRotationViewModelMessage : PassValueMessage<GraphFieldAxisRotatingViewModel>
    {
        public RemoveRotationViewModelMessage(GraphFieldAxisRotatingViewModel value) : base(value)
        {
        }
    }
}
