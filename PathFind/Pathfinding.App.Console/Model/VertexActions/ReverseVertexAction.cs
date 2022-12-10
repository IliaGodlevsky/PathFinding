using Pathfinding.App.Console.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;

namespace Pathfinding.App.Console.Model.VertexActions
{
    internal sealed class ReverseVertexAction : IVertexAction
    {
        private readonly IPathfindingRange<Vertex> range;

        public ReverseVertexAction(IPathfindingRange<Vertex> range)
        {
            this.range = range;
        }

        public void Do(Vertex vertex)
        {
            if (vertex.IsObstacle)
            {
                vertex.IsObstacle = false;
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
