using Algorithm.Interfaces;
using GraphLib.Interfaces;

namespace WPFVersion.Messages
{
    internal sealed class EndPointsChosenMessage
    {
        public IAlgorithm Algorithm { get; }
        public IIntermediateEndPoints EndPoints { get; }

        public EndPointsChosenMessage(IAlgorithm algorithm, IIntermediateEndPoints endPoints)
        {
            Algorithm = algorithm;
            EndPoints = endPoints;
        }
    }
}
