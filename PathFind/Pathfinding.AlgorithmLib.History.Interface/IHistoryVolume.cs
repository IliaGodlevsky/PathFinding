using System;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.History.Interface
{
    public interface IHistoryVolume<T>
    {
        IEnumerable<T> Get(Guid key);

        void Add(Guid key, T item);

        void Remove(Guid key);

        void RemoveAll();
    }
}
