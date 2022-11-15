using Pathfinding.AlgorithmLib.Core.Abstractions;

namespace Pathfinding.App.WPF._2D.Messages.ActionMessages
{
    internal sealed class SubscribeOnAlgorithmEventsMessage
    {
        public bool IsVisualizationRequired { get; }

        public PathfindingProcess Algorithm { get; }

        public SubscribeOnAlgorithmEventsMessage(
            PathfindingProcess algorithm, bool isVisualizationRequired)
        {
            Algorithm = algorithm;
            IsVisualizationRequired = isVisualizationRequired;
        }
    }
}
