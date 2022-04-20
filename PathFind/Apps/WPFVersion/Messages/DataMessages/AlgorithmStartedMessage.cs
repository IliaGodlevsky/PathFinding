using Algorithm.Base;

namespace WPFVersion.Messages.DataMessages
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
