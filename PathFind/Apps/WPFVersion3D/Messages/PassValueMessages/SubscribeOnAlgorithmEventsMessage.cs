using Algorithm.Base;

namespace WPFVersion3D.Messages.PassValueMessages
{
    internal sealed class SubscribeOnAlgorithmEventsMessage : PassValueMessage<PathfindingAlgorithm>
    {
        public bool IsVisualizationRequired { get; }

        public SubscribeOnAlgorithmEventsMessage(PathfindingAlgorithm value, bool isVisualizationRequired) : base(value)
        {
            IsVisualizationRequired = isVisualizationRequired;
        }
    }
}
