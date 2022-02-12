using Algorithm.Base;

namespace WPFVersion.Messages
{
    internal sealed class AlgorithmStartedMessage
    {
        public AlgorithmStartedMessage(PathfindingAlgorithm algorithm, int delayTime)
        {
            Algorithm = algorithm;
            DelayTime = delayTime;
        }

        public int DelayTime { get; }
        public PathfindingAlgorithm Algorithm { get; }
    }
}
