using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal interface IRepository<T>
        where T : class, IIdentityItem<Guid>
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> GetAll(Func<T, bool> predicate);

        T GetBy(Func<T, bool> predicate);

        T GetById(Guid id);

        void Add(T item);

        void Delete(T item);

        void Update(T item);
    }
}
