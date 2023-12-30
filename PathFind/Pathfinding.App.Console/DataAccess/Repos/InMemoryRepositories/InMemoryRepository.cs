using Pathfinding.App.Console.DataAccess.Entities;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Repos.InMemoryRepositories
{
    internal abstract class InMemoryRepository<T> where T : class, IEntity
    {
        protected static int Id = 0;

        public IDictionary<int, T> Repository { get; } = new Dictionary<int, T>();

        public T Create(T entity)
        {
            entity.Id = ++Id;
            Repository[Id] = entity;
            return entity;
        }

        public T Read(int id)
        {
            if (Repository.TryGetValue(id, out T entity))
            {
                return entity;
            }
            return null;
        }

        public bool Update(T entity)
        {
            if (Repository.ContainsKey(entity.Id))
            {
                Repository[entity.Id] = entity;
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            return Repository.Remove(id);
        }

        public virtual IReadOnlyDictionary<int, T> GetSnapshot()
        {
            return Repository.ToDictionary();
        }
    }
}
