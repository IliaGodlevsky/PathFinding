using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal sealed class ProxyRepository<T> : IRepository<T>
        where T : class, IIdentityItem<long>
    {
        private readonly IRepository<T> repository;

        private readonly Dictionary<long, T> items = new();

        public ProxyRepository(IRepository<T> repository)
        {
            this.repository = repository;
        }

        public T Add(T item)
        {
            T added = repository.Add(item);
            items.Add(added.Id, item);
            return added;
        }

        public T Delete(T item)
        {
            items.Remove(item.Id);
            return item;
        }

        public IEnumerable<T> GetAll()
        {
            if (items.Count == 0)
            {
                var all = repository.GetAll();
                all.ForEach(i => items[i.Id] = i);
                return items.Values;
            }
            return items.Values;
        }

        public IEnumerable<T> GetAll(Func<T, bool> predicate)
        {           
            if (items.Count == 0)
            {
                var all = repository.GetAll();
                all.ForEach(i => items[i.Id] = i);
                return items.Values.Where(predicate);
            }
            return items.Values.Where(predicate);
        }

        public T GetBy(Func<T, bool> predicate)
        {
            var item = items.Values.FirstOrDefault(predicate);
            if(item == null)
            {
                item = repository.GetBy(predicate);
                if (item != null)
                {
                    items.Add(item.Id, item);
                    return item;
                }
            }
            return item;
        }

        public T GetById(long id)
        {
            if (items.TryGetValue(id, out var item))
            {
                return item;
            }
            return null;
        }

        public T Update(T item)
        {
            if (items.ContainsKey(item.Id))
            {                
                var updated = repository.Update(item);
                items[item.Id] = updated;
                return updated;
            }

            return null;
        }

        public void Commit()
        {

        }

        public async ValueTask CommitAsync()
        {
            await Task.CompletedTask;
        }
    }
}
