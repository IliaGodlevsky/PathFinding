using NLog.Filters;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Interface
{
    internal interface IRepository<T>
        where T : class, IDto
    {
        T Read(Guid id);

        bool Create(T item);

        bool Update(T item);

        bool Delete(T item);

        IEnumerable<T> GetAll();

        void SaveChanges();
    }
}
