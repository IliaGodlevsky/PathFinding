using JsonFlatFileDataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal abstract class JsonRepository<T> : IRepository<T>
        where T : class, IIdentityItem<Guid>
    {
        private readonly DataStore context;

        protected abstract string Table { get; }

        protected JsonRepository(DataStore storage)
        {
            this.context = storage;
        }

        public virtual T Add(T item)
        {
            item.Id = Guid.NewGuid();
            var mapped = Map(item);
            var collection = context.GetCollection(Table);
            collection.InsertOne(mapped);
            return item;
        }

        public virtual void Commit()
        {
            
        }

        public virtual async ValueTask CommitAsync()
        {
            await Task.CompletedTask;
        }

        public virtual T Delete(T item)
        {
            var mapped = Map(item);
            var collection = context.GetCollection(Table);
            collection.DeleteOne(mapped);
            return item;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return context.GetCollection(Table)
                .AsQueryable()
                .Select(Map);
        }

        public virtual IEnumerable<T> GetAll(Func<T, bool> predicate)
        {
            return context.GetCollection(Table)
                .AsQueryable()
                .Select(Map)
                .Where(predicate);
        }

        public virtual T GetBy(Func<T, bool> predicate)
        {
            return context.GetCollection(Table)
                .AsQueryable()
                .Select(Map)
                .FirstOrDefault(predicate);
        }

        public virtual T GetById(Guid id)
        {
            var value = context.GetItem<dynamic>(id.ToString());
            return Map(value);
        }

        public virtual T Update(T item)
        {
            var mapped = Map(item);
            context.UpdateItem(item.Id.ToString(), item);
            return item;
        }

        protected abstract dynamic Map(T item);

        protected abstract T Map(dynamic model);
    }
}
