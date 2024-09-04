using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Shared;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Commands
{
    [Order(3), Group(Constants.ExcludeCommands)]
    public sealed class ExcludeTransitVertex<TVertex> : IPathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        public void Execute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            range.Transit.Remove(vertex);
        }

        public bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            return range.Transit.Contains(vertex);
        }
    }
}