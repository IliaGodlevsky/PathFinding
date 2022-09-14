using Algorithm.Interfaces;

namespace WPFVersion3D.Messages.PassValueMessages
{
    internal sealed class PathFoundMessage : PassValueMessage<IGraphPath>
    {
        public IAlgorithm<IGraphPath> Algorithm { get; }

        public PathFoundMessage(IGraphPath value, IAlgorithm<IGraphPath> algorithm) : base(value)
        {
            Algorithm = algorithm;
        }
    }
}
