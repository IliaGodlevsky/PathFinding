using Pathfinding.App.Console.DataAccess.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Repo
{
    internal interface IRepo<T> where T : class, IEntity
    {
        T Create(T entity);

        TEnumerable Create<TEnumerable>(TEnumerable entities)
            where TEnumerable : IEnumerable<T>;

        T Read(int id);

        bool Update(T entity);

        bool Delete(int id);

        IEnumerable<T> GetAll();
    }
}
