using Pathfinding.App.Console.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Repositories
{
    internal abstract class MemoryRepository<T> : IRepository<T>
        where T : class, IDto, new()
    {
        private sealed class EntityComparer : IEqualityComparer<T>
        {
            public bool Equals(T x, T y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode(T obj)
            {
                return obj.Id.GetHashCode();
            }
        }

        private readonly HashSet<T> items = new(new EntityComparer());

        public bool Create(T item)
        {
            return items.Add(item);
        }

        public bool Delete(T item)
        {
            return items.Remove(item);
        }

        public IEnumerable<T> GetAll()
        {
            return items;
        }

        public T Read(Guid id)
        {
            if (items.TryGetValue(new T{ Id = id }, out var value))
            {
                return value;
            }
            throw new KeyNotFoundException();
        }

        public void SaveChanges()
        {

        }

        public bool Update(T item)
        {
            if (items.TryGetValue(item, out var value))
            {
                value = Update(item, value);
                return true;
            }
            return false;
        }

        protected abstract T Update(T entity, T value);
    }
}
