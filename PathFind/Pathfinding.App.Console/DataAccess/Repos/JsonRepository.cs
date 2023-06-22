using JsonFlatFileDataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal abstract class JsonRepository<T, TMap> : IRepository<T>
        where T : class, IIdentityItem<long>
        where TMap : class, IIdentityItem<long>, new()
    {
        private readonly DataStore context;

        protected abstract string Table { get; }

        protected JsonRepository(DataStore storage)
        {
            this.context = storage;
        }

        public virtual T Add(T item)
        {
            var collection = context.GetCollection<TMap>(Table);
            int id = (int)collection.GetNextIdValue();
            item.Id = id++;
            var mapped = Map(item);           
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
            var collection = context.GetCollection<TMap>(Table);
            collection.DeleteOne(mapped);
            return item;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return context.GetCollection<TMap>(Table)
                .AsQueryable()
                .Select(Map);
        }

        public virtual IEnumerable<T> GetAll(Func<T, bool> predicate)
        {
            return context.GetCollection<TMap>(Table)
                .AsQueryable()
                .Select(Map)
                .Where(predicate);
        }

        public virtual T GetBy(Func<T, bool> predicate)
        {
            return context.GetCollection<TMap>(Table)
                .AsQueryable()
                .Select(Map)
                .FirstOrDefault(predicate);
        }

        public virtual T GetById(long id)
        {
            var collection = context.GetCollection<TMap>(Table);
            var value = collection.AsQueryable().FirstOrDefault(c => c.Id == id);
            return Map(value);
        }

        public virtual T Update(T item)
        {
            var mapped = Map(item);
            var collection = context.GetCollection<TMap>(Table);
            var i = GetById(item.Id);
            collection.DeleteOne(item.Id);
            collection.InsertOne(mapped);
            return item;
        }

        protected abstract TMap Map(T item);

        protected abstract T Map(TMap model);
    }
}
