using System;

namespace Pathfinding.App.Console.Interface
{
    internal interface IHistoryVolume<TKey, TValue>
    {
        bool Add(TKey id, TValue item);

        bool Remove(TKey id);

        TValue Get(TKey id);

        void RemoveAll();
    }
}
