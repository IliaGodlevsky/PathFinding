using Algorithm.Base;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Messages
{
    internal sealed class AlgorithmStartedMessage : IPassValueMessage<PathfindingAlgorithm>
    {
        public PathfindingAlgorithm Value { get; }

        public AlgorithmStartedMessage(PathfindingAlgorithm algorithm)
        {
            Value = algorithm;
        }        
    }
}
