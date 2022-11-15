using Pathfinding.App.WPF._2D.ViewModel;

namespace Pathfinding.App.WPF._2D.Messages.ActionMessages
{
    internal sealed class RemoveAlgorithmMessage
    {
        public AlgorithmViewModel Model { get; }

        public RemoveAlgorithmMessage(AlgorithmViewModel model)
        {
            Model = model;
        }
    }
}
