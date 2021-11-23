using Algorithm.Base;

namespace WPFVersion.Messages
{
    internal sealed class SubscribeOnAlgorithmEventsMessage
    {
        public bool IsVisualizationRequired { get; }
        public PathfindingAlgorithm Algorithm { get; }

        public SubscribeOnAlgorithmEventsMessage(
            PathfindingAlgorithm algorithm, bool isVisualizationRequired)
        {
            Algorithm = algorithm;
            IsVisualizationRequired = isVisualizationRequired;
        }
    }
}
