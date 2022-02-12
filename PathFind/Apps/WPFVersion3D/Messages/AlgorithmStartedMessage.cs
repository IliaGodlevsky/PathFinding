using Algorithm.Base;

namespace WPFVersion3D.Messages
{
    internal sealed class AlgorithmStartedMessage
    {
        public AlgorithmStartedMessage(PathfindingAlgorithm algorithm)
        {
            Algorithm = algorithm;
        }

        public PathfindingAlgorithm Algorithm { get; }
    }
}
