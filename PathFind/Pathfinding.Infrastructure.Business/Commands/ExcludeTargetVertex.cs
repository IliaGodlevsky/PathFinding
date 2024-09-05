using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Shared;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Commands
{
    [Order(2)]
    public class ExcludeTargetVertex<TVertex> : IPathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        public void Execute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            range.Target = default;
        }

        public bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            return range.Target?.Equals(vertex) == true;
        }
    }
}