using Algorithm.Base;

namespace WPFVersion.Messages
{
    internal sealed class AlgorithmStartedMessage
    {
        public AlgorithmStartedMessage(PathfindingAlgorithm algorithm,
            string algorithmName, int delayTime)
        {
            Algorithm = algorithm;
            AlgorithmName = algorithmName;
            DelayTime = delayTime;
        }

        public int DelayTime { get; }
        public string AlgorithmName { get; }
        public PathfindingAlgorithm Algorithm { get; }
    }
}
