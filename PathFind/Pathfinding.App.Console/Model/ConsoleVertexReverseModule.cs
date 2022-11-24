using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.Visualization.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;

namespace Pathfinding.App.Console.Model
{
    internal sealed class ConsoleVertexReverseModule
    {
        private readonly IPathfindingRange range;

        public ConsoleVertexReverseModule(IPathfindingRange range)
        {
            this.range = range;
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
                if (!range.IsInRange(vertex))
                {
                    vertex.IsObstacle = true;
                }
            }
        }
    }
}
