using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;

namespace Pathfinding.App.Console.Model
{
    internal sealed class ConsoleVertexReverseModule
    {
        private readonly IPathfindingRange<Vertex> range;

        public ConsoleVertexReverseModule(IPathfindingRange<Vertex> range)
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
