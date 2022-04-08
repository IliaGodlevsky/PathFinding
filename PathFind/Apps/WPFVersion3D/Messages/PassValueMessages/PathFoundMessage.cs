using Algorithm.Interfaces;

namespace WPFVersion3D.Messages.PassValueMessages
{
    internal sealed class PathFoundMessage : PassValueMessage<IGraphPath>
    {
        public IAlgorithm Algorithm { get; }

        public PathFoundMessage(IGraphPath value, IAlgorithm algorithm) : base(value)
        {
            Algorithm = algorithm;
        }
    }
}
