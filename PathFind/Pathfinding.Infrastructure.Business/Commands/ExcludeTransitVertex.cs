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
        public void Execute(IList<TVertex> range, TVertex vertex)
        {
            range.Remove(vertex);
        }

        public bool CanExecute(IList<TVertex> range, TVertex vertex)
        {
            return range.Contains(vertex);
        }
    }
}