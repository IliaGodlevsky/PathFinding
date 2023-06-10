using System;

namespace Pathfinding.App.Console.Model
{
    internal interface IRepository<T>
    {
        bool Add(Guid id, T item);

        bool Remove(Guid id);

        T Get(Guid id);

        void RemoveAll();
    }
}
