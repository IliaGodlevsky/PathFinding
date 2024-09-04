using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Shared;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Commands
{
    [Order(1), Group(Constants.ExcludeCommands)]
    public sealed class ExcludeSourceVertex<TVertex> : IPathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        public void Execute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            range.Source = default;
        }

        public bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            return range.Source?.Equals(vertex) == true;
        }
    }
}