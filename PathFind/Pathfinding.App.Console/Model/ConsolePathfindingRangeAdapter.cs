using Pathfinding.App.Console.EventArguments;
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

        protected override void SubscribeVertex(Vertex vertex)
        {
            vertex.IncludedInRange += SetPathfindingRange;
            vertex.MarkedAsIntermediateToReplace += MarkIntermediateVertexToReplace;
        }

        protected override void UnsubscribeVertex(Vertex vertex)
        {
            vertex.IncludedInRange -= SetPathfindingRange;
            vertex.MarkedAsIntermediateToReplace -= MarkIntermediateVertexToReplace;
        }

        private void SetPathfindingRange(object sender, VertexEventArgs e)
        {
            SetPathfindingRange(e.Current);
        }

        private void MarkIntermediateVertexToReplace(object sender, VertexEventArgs e)
        {
            MarkIntermediateVertexToReplace(e.Current);
        }
    }
}
