using Pathfinding.App.Console.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess.Repos.InMemoryRepositories
{
    internal sealed class InMemoryAlgorithmRepository 
        : InMemoryRepository<AlgorithmEntity>, IAlgorithmsRepository
    {
        public int AddMany(IEnumerable<AlgorithmEntity> entity)
        {
            int count = 0;
            foreach(var item in entity)
            {
                Create(item);
                count++;
            }
            return count;
        }

        public void AddOne(AlgorithmEntity entity)
        {
            Create(entity);
        }

        public bool DeleteByGraphId(int graphId)
        {
            var algorithm = repository.Values.FirstOrDefault(x => x.GraphId == graphId);
            if (algorithm is not null)
            {
                return Delete(algorithm.Id);
            }
            return false;
        }

        public IEnumerable<AlgorithmEntity> GetByGraphId(int graphId)
        {
            return repository.Values.Where(x => x.GraphId == graphId);
        }
    }
}
