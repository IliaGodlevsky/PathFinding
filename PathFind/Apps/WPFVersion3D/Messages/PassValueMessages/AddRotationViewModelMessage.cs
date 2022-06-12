using WPFVersion3D.ViewModel;

namespace WPFVersion3D.Messages.PassValueMessages
{
    internal sealed class AddRotationViewModelMessage : PassValueMessage<GraphFieldAxisRotatingViewModel>
    {
        public AddRotationViewModelMessage(GraphFieldAxisRotatingViewModel value) : base(value)
        {
        }
    }
}
