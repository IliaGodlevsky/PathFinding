using Pathfinding.App.Console.DataAccess.Entities;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess.Repos.InMemoryRepositories
{
    internal sealed class InMemoryNeighborsRepository
        : InMemoryRepository<NeighborEntity>, INeighborsRepository
    {
        private readonly IDictionary<int, VertexEntity> vertices;

        public InMemoryNeighborsRepository(IDictionary<int, VertexEntity> vertices)
        {
            this.vertices = vertices;
        }

        public NeighborEntity AddNeighbour(NeighborEntity neighbour)
        {
            return Create(neighbour);
        }

        public IEnumerable<NeighborEntity> AddNeighbours(IEnumerable<NeighborEntity> neighbours)
        {
            return neighbours.ForEach(n => Create(n));
        }

        public bool DeleteByGraphId(int graphId)
        {
            var vertex = vertices.Values.Where(x => x.GraphId == graphId)
                .Select(x => x.Id).ToHashSet();
            var neighbors = Repository.Values.Where(x => vertex.Contains(x.VertexId));
            foreach (var neighbor in neighbors)
            {
                Delete(neighbor.Id);
            }
            return true;
        }

        public bool DeleteNeighbour(int vertexId, int neighbourId)
        {
            var value = Repository.Values
                .FirstOrDefault(x => x.VertexId == vertexId && x.NeighborId == neighbourId);
            if (value is not null)
            {
                return Delete(value.Id);
            }
            return false;
        }

        public IReadOnlyDictionary<int, IReadOnlyCollection<NeighborEntity>> GetNeighboursForVertices(IEnumerable<int> verticesIds)
        {
            return Repository.Values
                .GroupBy(x => x.VertexId)
                .ToDictionary(x => x.Key, x => x.ToReadOnly())
                .AsReadOnly();
        }
    }
}
