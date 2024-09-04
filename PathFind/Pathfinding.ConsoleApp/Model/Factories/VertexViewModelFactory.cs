using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Shared.Primitives;

namespace Pathfinding.ConsoleApp.Model.Factories
{
    internal sealed class VertexModelFactory : IVertexFactory<VertexModel>
    {
        public VertexModel CreateVertex(Coordinate coordinate)
        {
            return new VertexModel(coordinate);
        }
    }
}
