using Algorithm.Interfaces;
using GraphLib.Interfaces;

namespace WPFVersion3D.Messages.PassValueMessages
{
    internal sealed class EndPointsChosenMessage : PassValueMessage<IEndPoints>
    {
        public IAlgorithm Algorithm { get; }

        public EndPointsChosenMessage(IEndPoints value, IAlgorithm algorithm) : base(value)
        {
            Algorithm = algorithm;
        }
    }
}
