using Algorithm.Base;
using GraphLib.Interfaces;

namespace WPFVersion.Messages
{
    internal sealed class AlgorithmChosenMessage
    {
        public PathfindingAlgorithm Algorithm { get; }

        public AlgorithmChosenMessage(PathfindingAlgorithm algorithm, IIntermediateEndPoints endPoints)
        {
            Algorithm = algorithm;
            EndPoints = endPoints;
        }

        public IIntermediateEndPoints EndPoints { get; }
    }
}
