using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.Visualization.Core.Abstractions;
using System.Diagnostics;

namespace Pathfinding.App.Console.Model
{
    [DebuggerDisplay("Source - {Source}, Target - {Target}")]
    internal sealed class ConsolePathfindingRange : VisualPathfindingRange<Vertex>
    {
        public void MarkAsIntermediateToReplace(Vertex vertex)
        {
            MarkIntermediateVertexToReplace(vertex);
        }
    }
}
