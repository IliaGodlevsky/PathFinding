using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Shared.Primitives;

namespace Pathfinding.Infrastructure.Business.Benchmarks.Data
{
    internal sealed class BenchmarkVertexFactory : IVertexFactory<BenchmarkVertex>
    {
        public BenchmarkVertex CreateVertex(Coordinate coordinate)
        {
            return new BenchmarkVertex(coordinate);
        }
    }
}
