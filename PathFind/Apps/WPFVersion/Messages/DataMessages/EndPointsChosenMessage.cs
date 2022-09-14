using Algorithm.Interfaces;
using GraphLib.Interfaces;

namespace WPFVersion.Messages.DataMessages
{
    internal sealed class EndPointsChosenMessage
    {
        public IAlgorithm<IGraphPath> Algorithm { get; }
        public IEndPoints EndPoints { get; }

        public EndPointsChosenMessage(IAlgorithm<IGraphPath> algorithm, IEndPoints endPoints)
        {
            Algorithm = algorithm;
            EndPoints = endPoints;
        }
    }
}
