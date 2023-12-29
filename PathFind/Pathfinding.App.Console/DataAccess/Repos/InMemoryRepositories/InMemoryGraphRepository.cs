using Pathfinding.App.Console.DataAccess.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Repos.InMemoryRepositories
{
    internal sealed class InMemoryGraphRepository
        : InMemoryRepository<GraphEntity>, IGraphParametresRepository
    {
        public GraphEntity AddGraph(GraphEntity graph)
        {
            return Create(graph);
        }

        public bool DeleteGraph(int graphId)
        {
            return Delete(graphId);
        }

        public IEnumerable<GraphEntity> GetAll()
        {
            return repository.Values;
        }

        public GraphEntity GetGraph(int graphId)
        {
            if (repository.TryGetValue(graphId, out var graph))
            {
                return graph;
            }
            throw new KeyNotFoundException();
        }
    }
}
