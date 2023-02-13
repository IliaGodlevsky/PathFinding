using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.History.Interface
{
    public interface IHistory<TColor>
    {
        IEnumerable<(ICoordinate, TColor)> Get(Guid key);

        void Add(Guid key, ICoordinate position, TColor color);

        void Remove(Guid key);

        void RemoveAll();
    }
}
