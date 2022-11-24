using Pathfinding.App.WPF._3D.ViewModel;

namespace Pathfinding.App.WPF._3D.Messages.PassValueMessages
{
    internal sealed class AddRotationViewModelMessage : PassValueMessage<GraphFieldAxisRotatingViewModel>
    {
        public AddRotationViewModelMessage(GraphFieldAxisRotatingViewModel value) : base(value)
        {
        }
    }
}
