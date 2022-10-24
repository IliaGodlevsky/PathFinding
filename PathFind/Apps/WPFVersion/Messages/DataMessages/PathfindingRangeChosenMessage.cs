using Algorithm.Interfaces;
using GraphLib.Interfaces;

namespace WPFVersion.Messages.DataMessages
{
    internal sealed class PathfindingRangeChosenMessage
    {
        public IAlgorithm<IGraphPath> Algorithm { get; }

        public IPathfindingRange Range { get; }

        public PathfindingRangeChosenMessage(IAlgorithm<IGraphPath> algorithm, IPathfindingRange range)
        {
            Algorithm = algorithm;
            Range = range;
        }
    }
}
