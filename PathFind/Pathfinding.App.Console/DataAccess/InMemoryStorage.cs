using Pathfinding.App.Console.DataAccess.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess
{
    internal abstract class InMemoryStorage<TEntity, TId> : IStorage<TEntity, TId>
        where TEntity : IEntity<TId>
    {
        protected abstract TId NextId { get; }

        private readonly Dictionary<TId, TEntity> storage = new();

        public TEntity Create(TEntity entity)
        {
            entity.Id = NextId;
            storage.Add(entity.Id, entity);
            return entity;
        }

        public TEntity Read(TId id)
        {
            if (storage.TryGetValue(id, out TEntity entity))
            {
                return entity;
            }

            throw new KeyNotFoundException();
        }

        public void Update(TEntity entity)
        {
            storage[entity.Id] = entity;
        }

        public TId Delete(TEntity entity)
        {
            storage.Remove(entity.Id);
            return entity.Id;
        }
    }
}
