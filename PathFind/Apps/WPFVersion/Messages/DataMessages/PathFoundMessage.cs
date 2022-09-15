using Algorithm.Interfaces;

namespace WPFVersion.Messages.DataMessages
{
    internal sealed class PathFoundMessage
    {
        public IAlgorithm<IGraphPath> Algorithm { get; }

        public IGraphPath Path { get; }

        public PathFoundMessage(IAlgorithm<IGraphPath> algorithm, IGraphPath path)
        {
            Algorithm = algorithm;
            Path = path;
        }
    }
}
