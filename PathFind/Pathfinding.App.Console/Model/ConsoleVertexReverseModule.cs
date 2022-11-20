using Pathfinding.Visualization.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;

namespace Pathfinding.App.Console.Model
{
    internal sealed class ConsoleVertexReverseModule
    {
        private readonly IPathfindingRangeAdapter<Vertex> adapter;

        public ConsoleVertexReverseModule(IPathfindingRangeAdapter<Vertex> adapter)
        {
            this.adapter = adapter;
        }

        public void ReverseVertex(Vertex vertex)
        {
            if (vertex.IsObstacle)
            {
                vertex.IsObstacle = false;
                vertex.VisualizeAsRegular();
            }
            else
            {
                if (!adapter.IsInRange(vertex))
                {
                    vertex.IsObstacle = true;
                }
            }
        }
    }
}
