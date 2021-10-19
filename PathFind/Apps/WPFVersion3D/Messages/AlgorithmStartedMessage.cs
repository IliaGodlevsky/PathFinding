using Algorithm.Base;

namespace WPFVersion3D.Messages
{
    internal sealed class AlgorithmStartedMessage
    {
        public AlgorithmStartedMessage(PathfindingAlgorithm algorithm, string algorithmName)
        {
            Algorithm = algorithm;
            AlgorithmName = algorithmName;
        }

        public string AlgorithmName { get; }
        public PathfindingAlgorithm Algorithm { get; }
    }
}
