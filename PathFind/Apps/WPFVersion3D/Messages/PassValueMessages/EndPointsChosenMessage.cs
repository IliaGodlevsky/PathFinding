using Algorithm.Interfaces;
using GraphLib.Interfaces;

namespace WPFVersion3D.Messages.PassValueMessages
{
    internal sealed class EndPointsChosenMessage : PassValueMessage<IEndPoints>
    {
        public IAlgorithm<IGraphPath> Algorithm { get; }

        public EndPointsChosenMessage(IEndPoints value, IAlgorithm<IGraphPath> algorithm) : base(value)
        {
            Algorithm = algorithm;
        }
    }
}
