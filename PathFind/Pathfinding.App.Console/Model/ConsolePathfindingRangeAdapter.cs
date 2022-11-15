using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.Visualization.Core.Abstractions;
using System.Diagnostics;

namespace Pathfinding.App.Console.Model
{
    [DebuggerDisplay("Source - {Source}, Target - {Target}")]
    internal sealed class ConsolePathfindingRangeAdapter : PathfindingRangeAdapter<Vertex>
    {
        public ConsolePathfindingRangeAdapter(IPathfindingRangeFactory rangeFactory) 
            : base(rangeFactory)
        {
        }

        public void IncludeInRange(Vertex vertex)
        {
            SetPathfindingRange(vertex);
        }

        public void MarkAsIntermediateToReplace(Vertex vertex)
        {
            MarkIntermediateVertexToReplace(vertex);
        }
    }
}
