using GraphLib.Base.EndPoints;
using GraphLib.Interfaces;
using System.Diagnostics;

namespace ConsoleVersion.Model
{
    [DebuggerDisplay("Source - {Source}, Target - {Target}")]
    internal sealed class PathfindingRange : BasePathfindingRange
    {
        protected override void SubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.RangeChosen += SetPathfindingRange;
                vert.MarkedToReplaceIntermediate += MarkIntermediateToReplace;
            }
        }

        protected override void UnsubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.RangeChosen -= SetPathfindingRange;
                vert.MarkedToReplaceIntermediate -= MarkIntermediateToReplace;
            }
        }
    }
}
