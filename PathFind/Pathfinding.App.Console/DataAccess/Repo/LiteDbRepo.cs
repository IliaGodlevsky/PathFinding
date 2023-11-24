using LiteDB;
using Pathfinding.App.Console.DataAccess.Entities;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.DataAccess.Repo
{
    internal sealed class LiteDbRepo<T> : IRepo<T> where T : class, IEntity
    {
        private readonly ILiteCollection<T> repo;

        public LiteDbRepo(LiteDatabase database, string name)
        {
            repo = database.GetCollection<T>(name, BsonAutoId.Int32);
        }

        public T Create(T entity)
        {
            repo.Insert(entity);
            return entity;
        }

        public TEnumerable Create<TEnumerable>(TEnumerable entities)
            where TEnumerable : IEnumerable<T>
        {
            repo.Insert(entities);
            return entities;
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }

        public IEnumerable<T> GetAll()
        {
            return repo.FindAll().ToList();
        }

        public T Read(int id)
        {
            return repo.FindById(id);
        }

        public bool Update(T entity)
        {
            return repo.Update(entity);
        }
    }
}
