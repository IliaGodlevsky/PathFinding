using Pathfinding.AlgorithmLib.Core.Abstractions;

namespace Pathfinding.App.WPF._3D.Messages.PassValueMessages
{
    internal sealed class SubscribeOnAlgorithmEventsMessage : PassValueMessage<PathfindingProcess>
    {
        public bool IsVisualizationRequired { get; }

        public SubscribeOnAlgorithmEventsMessage(PathfindingProcess value, bool isVisualizationRequired) : base(value)
        {
            IsVisualizationRequired = isVisualizationRequired;
        }
    }
}
