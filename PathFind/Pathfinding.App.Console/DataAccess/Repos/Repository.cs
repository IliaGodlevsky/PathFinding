using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal sealed class Repository<T> : IRepository<T>
        where T : class, IIdentityItem<Guid>
    {
        private readonly Dictionary<Guid, T> items = new();

        public Repository() 
        { 

        }
        public void Add(T item)
        {
            item.Id = Guid.NewGuid();
            items.Add(item.Id, item);
        }

        public void Delete(T item)
        {
            items.Remove(item.Id);
        }

        public IEnumerable<T> GetAll()
        {
            return items.Values;
        }

        public IEnumerable<T> GetAll(Func<T, bool> predicate)
        {
            return items.Values.Where(predicate);
        }

        public T GetBy(Func<T, bool> predicate)
        {
            return items.Values.FirstOrDefault(predicate);
        }

        public T GetById(Guid id)
        {
            if (items.TryGetValue(id, out var item))
            {
                return item;
            }
            return null;
        }

        public void Update(T item)
        {
            if (items.ContainsKey(item.Id))
            {
                items[item.Id] = item;
            }
        }
    }
}
