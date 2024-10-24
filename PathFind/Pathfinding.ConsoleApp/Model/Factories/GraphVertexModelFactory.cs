using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Shared.Primitives;

namespace Pathfinding.ConsoleApp.Model.Factories
{
    internal sealed class GraphVertexModelFactory : IVertexFactory<GraphVertexModel>
    {
        public GraphVertexModel CreateVertex(Coordinate coordinate)
        {
            return new GraphVertexModel(coordinate);
        }
    }
}
