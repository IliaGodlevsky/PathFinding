using Algorithm.Base;

namespace WPFVersion.Messages
{
    internal sealed class SubscribeOnAlgorithmEventsMessage
    {
        public PathfindingAlgorithm Algorithm { get; }

        public SubscribeOnAlgorithmEventsMessage(PathfindingAlgorithm algorithm)
        {
            Algorithm = algorithm;
        }
    }
}
