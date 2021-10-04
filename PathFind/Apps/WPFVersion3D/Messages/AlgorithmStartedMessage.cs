using Algorithm.Interfaces;
using WPFVersion3D.Enums;

namespace WPFVersion3D.Messages
{
    internal sealed class AlgorithmStartedMessage
    {
        public AlgorithmStartedMessage(IAlgorithm algorithm, string algorithmName)
        {
            Algorithm = algorithm;
            AlgorithmName = algorithmName;
            Status = AlgorithmStatuses.Started;
        }

        public AlgorithmStatuses Status { get; }
        public string AlgorithmName { get; }
        public IAlgorithm Algorithm { get; }
    }
}
