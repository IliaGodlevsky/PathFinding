using Pathfinding.App.Console.DataAccess.Entities;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess.Repos.InMemoryRepositories
{
    internal sealed class InMemoryRangeRepository
        : InMemoryRepository<RangeEntity>, IRangeRepository
    {
        public RangeEntity AddRange(RangeEntity entity)
        {
            return Create(entity);
        }

        public IEnumerable<RangeEntity> AddRange(IEnumerable<RangeEntity> entities)
        {
            entities.ForEach(x => Create(x));
            return entities;
        }

        public bool DeleteByGraphId(int graphId)
        {
            var values = repository.Values.Where(x => x.GraphId == graphId).ToReadOnly();
            foreach (var entity in values)
            {
                Delete(entity.Id); ;
            }
            return true;
        }

        public bool DeleteByVertexId(int vertexId)
        {
            var vertex = repository.Values.FirstOrDefault(x => x.VertexId == vertexId);
            if(vertex is not null)
            {
                return Delete(vertex.Id);
            }
            return false;
        }

        public IEnumerable<RangeEntity> GetByGraphId(int graphId)
        {
            return repository.Values.Where(x => x.GraphId == graphId);
        }

        public RangeEntity GetByVertexId(int vertexId)
        {
            return repository.Values.First(x => x.VertexId == vertexId);
        }
    }
}
