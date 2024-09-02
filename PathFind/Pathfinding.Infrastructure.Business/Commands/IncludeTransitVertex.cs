using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Shared;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Commands
{
    [Order(6), Group(Constants.IncludeCommands)]
    public sealed class IncludeTransitVertex<TVertex> : IPathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        public void Execute(IList<TVertex> range, TVertex vertex)
        {
            range.Insert(range.Count - 1, vertex);
        }

        public bool CanExecute(IList<TVertex> range, TVertex vertex)
        {
            return range.Count >= 2
                && range.CanBeInRange(vertex);
        }
    }
}