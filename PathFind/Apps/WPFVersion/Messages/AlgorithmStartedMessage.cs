using Algorithm.Interfaces;

namespace WPFVersion.Messages
{
    internal sealed class AlgorithmStartedMessage
    {
        public AlgorithmStartedMessage(IAlgorithm algorithm,
            string algorithmName, int delayTime)
        {
            Algorithm = algorithm;
            AlgorithmName = algorithmName;
            DelayTime = delayTime;
        }

        public int DelayTime { get; }
        public string AlgorithmName { get; }
        public IAlgorithm Algorithm { get; }
    }
}
