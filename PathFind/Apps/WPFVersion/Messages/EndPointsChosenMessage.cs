using Algorithm.Interfaces;
using GraphLib.Interfaces;

namespace WPFVersion.Messages
{
    internal sealed class EndPointsChosenMessage
    {
        public IAlgorithm Algorithm { get; }
        public IEndPoints EndPoints { get; }

        public EndPointsChosenMessage(IAlgorithm algorithm, IEndPoints endPoints)
        {
            Algorithm = algorithm;
            EndPoints = endPoints;
        }
    }
}
