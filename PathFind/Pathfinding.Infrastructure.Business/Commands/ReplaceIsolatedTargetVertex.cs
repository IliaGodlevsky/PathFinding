using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Shared;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Commands
{
    [Order(5), Group(Constants.IncludeCommands)]
    public sealed class ReplaceIsolatedTargetVertex<TVertex> : IPathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        public void Execute(IList<TVertex> range, TVertex vertex)
        {
            range.RemoveAt(range.Count - 1);
            range.Add(vertex);
        }

        public bool CanExecute(IList<TVertex> range, TVertex vertex)
        {
            return range.Count >= 2
                && range[range.Count - 1].IsIsolated()
                && range.CanBeInRange(vertex);
        }
    }
}