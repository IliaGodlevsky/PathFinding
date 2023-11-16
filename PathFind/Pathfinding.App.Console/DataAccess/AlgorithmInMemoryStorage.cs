using Pathfinding.App.Console.DataAccess.Entities;
using System;

namespace Pathfinding.App.Console.DataAccess
{
    internal sealed class AlgorithmInMemoryStorage : InMemoryStorage<AlgorithmEntity, Guid>
    {
        protected override Guid NextId => Guid.NewGuid();
    }
}
