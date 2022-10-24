using Algorithm.Interfaces;
using GraphLib.Interfaces;

namespace WPFVersion3D.Messages.PassValueMessages
{
    internal sealed class PathfindingRangeChosenMessage : PassValueMessage<IPathfindingRange>
    {
        public IAlgorithm<IGraphPath> Algorithm { get; }

        public PathfindingRangeChosenMessage(IPathfindingRange range, IAlgorithm<IGraphPath> algorithm) : base(range)
        {
            Algorithm = algorithm;
        }
    }
}
