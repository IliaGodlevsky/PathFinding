using Pathfinding.App.WPF._3D.ViewModel;

namespace Pathfinding.App.WPF._3D.Messages.PassValueMessages
{
    internal sealed class RemoveRotationViewModelMessage : PassValueMessage<GraphFieldAxisRotatingViewModel>
    {
        public RemoveRotationViewModelMessage(GraphFieldAxisRotatingViewModel value) : base(value)
        {
        }
    }
}
