using Pathfinding.App.Console.Interface;
using Pathfinding.Service.Interface.Commands;

namespace Pathfinding.App.Console.Model.VertexActions
{
    internal sealed class ExcludeFromRangeAction : IVertexAction
    {
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;

        public ExcludeFromRangeAction(IPathfindingRangeBuilder<Vertex> rangeBuilder)
        {
            this.rangeBuilder = rangeBuilder;
        }

        public void Invoke(Vertex vertex)
        {
            rangeBuilder.Exclude(vertex);
        }
    }
}
