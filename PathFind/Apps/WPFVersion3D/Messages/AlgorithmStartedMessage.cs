using Algorithm.Base;

namespace WPFVersion3D.Messages
{
    internal sealed class AlgorithmStartedMessage : PassValueMessage<PathfindingAlgorithm>
    {
        public AlgorithmStartedMessage(PathfindingAlgorithm algorithm): base(algorithm)
        {

        }
    }
}
