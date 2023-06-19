using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal interface IRepository<T>
        where T : class, IIdentityItem<Guid>
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> GetAll(Func<T, bool> predicate);

        T GetBy(Func<T, bool> predicate);

        T GetById(Guid id);

        T Add(T item);

        T Delete(T item);

        T Update(T item);

        void Commit();

        ValueTask CommitAsync();
    }
}
