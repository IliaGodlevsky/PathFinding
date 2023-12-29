using Pathfinding.App.Console.DataAccess.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Repos.InMemoryRepositories
{
    internal abstract class InMemoryRepository<T> where T : class, IEntity
    {
        protected static int Id = 0;

        protected readonly Dictionary<int, T> repository = new();

        public IReadOnlyDictionary<int, T> Repos => repository;

        public T Create(T entity)
        {
            entity.Id = ++Id;
            repository[Id] = entity;
            return entity;
        }

        public T Read(int id)
        {
            if (repository.TryGetValue(id, out T entity))
            {
                return entity;
            }
            return null;
        }

        public bool Update(T entity)
        {
            if (repository.ContainsKey(entity.Id))
            {
                repository[entity.Id] = entity;
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            return repository.Remove(id);
        }
    }
}
