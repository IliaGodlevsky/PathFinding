using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Shared.Primitives;

namespace Pathfinding.ConsoleApp.Model.Factories
{
    internal sealed class RunVertexModelFactory : IVertexFactory<RunVertexModel>
    {
        public RunVertexModel CreateVertex(Coordinate coordinate)
        {
            return new(coordinate);
        }
    }
}
