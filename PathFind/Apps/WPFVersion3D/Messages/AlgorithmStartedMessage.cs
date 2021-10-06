using Algorithm.Interfaces;

namespace WPFVersion3D.Messages
{
    internal sealed class AlgorithmStartedMessage
    {
        public AlgorithmStartedMessage(IAlgorithm algorithm, string algorithmName)
        {
            Algorithm = algorithm;
            AlgorithmName = algorithmName;
        }

        public string AlgorithmName { get; }
        public IAlgorithm Algorithm { get; }
    }
}
