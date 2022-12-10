using Pathfinding.App.Console.Interface;
using Pathfinding.GraphLib.Core.Modules.Interface;

namespace Pathfinding.App.Console.Model.VertexActions
{
    internal sealed class ExcludeFromRangeAction : IVertexAction
    {
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;

        public ExcludeFromRangeAction(IPathfindingRangeBuilder<Vertex> rangeBuilder)
        {
            this.rangeBuilder = rangeBuilder;
        }

        public void Do(Vertex vertex)
        {
            rangeBuilder.Exclude(vertex);
        }
    }
}
