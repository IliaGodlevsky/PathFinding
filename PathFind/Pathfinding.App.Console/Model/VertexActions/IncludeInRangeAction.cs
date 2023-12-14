using Pathfinding.App.Console.Interface;
using Pathfinding.GraphLib.Core.Modules.Interface;
using System;

namespace Pathfinding.App.Console.Model.VertexActions
{
    internal sealed class IncludeInRangeAction : IVertexAction
    {
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;

        public IncludeInRangeAction(IPathfindingRangeBuilder<Vertex> rangeBuilder)
        {
            this.rangeBuilder = rangeBuilder;
        }

        public void Invoke(Vertex vertex)
        {
            rangeBuilder.Include(vertex);
        }
    }
}
