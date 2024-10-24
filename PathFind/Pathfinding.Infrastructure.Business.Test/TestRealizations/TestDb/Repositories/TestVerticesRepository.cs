using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface.Repositories;
using Pathfinding.Shared.Extensions;

namespace Pathfinding.Infrastructure.Business.Test.TestRealizations.TestDb.Repositories
{
    internal sealed class TestVerticesRepository : IVerticesRepository
    {
        private int id = 0;

        public async Task<IEnumerable<Vertex>> CreateAsync(IEnumerable<Vertex> vertices,
            CancellationToken token = default)
        {
            var result = vertices
                .ForEach(x => x.Id = ++id)
                .ToList();
            return await Task.FromResult(result);
        }

        public async Task<bool> DeleteVerticesByGraphIdAsync(int graphId,
            CancellationToken token = default)
        {
            return await Task.FromResult(true);
        }

        public async Task<Vertex> ReadAsync(int vertexId,
            CancellationToken token = default)
        {
            var vertex = new Vertex()
            {
                Id = vertexId,
                GraphId = 1,
                Coordinates = "[0,0]",
                Cost = 1,
                IsObstacle = false,
                LowerValueRange = 1,
                UpperValueRange = 2
            };
            return await Task.FromResult(vertex);
        }

        public async Task<IEnumerable<Vertex>> ReadVerticesByGraphIdAsync(int graphId,
            CancellationToken token = default)
        {
            var vertex = new Vertex()
            {
                Id = 1,
                GraphId = graphId,
                Coordinates = "[0,0]",
                Cost = 1,
                IsObstacle = false,
                LowerValueRange = 1,
                UpperValueRange = 2
            };
            return await Task.FromResult(vertex.Enumerate());
        }

        public Task<bool> UpdateVerticesAsync(IEnumerable<Vertex> vertices,
            CancellationToken token = default)
        {
            return Task.FromResult(true);
        }
    }
}
