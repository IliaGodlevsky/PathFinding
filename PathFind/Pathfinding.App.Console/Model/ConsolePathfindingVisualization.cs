using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Visualization;
using Pathfinding.GraphLib.Core.Realizations.Graphs;

namespace Pathfinding.App.Console.Model
{
    internal sealed class ConsolePathfindingVisualization : PathfindingVisualization<Graph2D<Vertex>, Vertex>
    {
        public ConsolePathfindingVisualization(Graph2D<Vertex> graph) : base(graph)
        {
        }

        public void SubscribeOnHistory(PathfindingProcess algorithm)
        {
            SubscribeOnAlgorithmEvents(algorithm);
        }
    }
}
