using Algorithm.Base;

namespace WPFVersion.Messages
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
