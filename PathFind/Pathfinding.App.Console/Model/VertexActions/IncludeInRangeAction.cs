using Pathfinding.App.Console.Interface;
using Pathfinding.Service.Interface.Commands;

namespace Pathfinding.App.Console.Model.VertexActions
{
    internal sealed class IncludeInRangeAction(IPathfindingRangeBuilder<Vertex> rangeBuilder) : IVertexAction
    {
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder = rangeBuilder;

        public void Invoke(Vertex vertex)
        {
            rangeBuilder.Include(vertex);
        }
    }
}
