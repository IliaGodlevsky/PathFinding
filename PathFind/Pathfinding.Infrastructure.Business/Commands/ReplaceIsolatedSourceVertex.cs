using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Shared;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Commands
{
    [Order(3)]
    public sealed class ReplaceIsolatedSourceVertex<TVertex> : IPathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        public void Execute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            range.Source = default;
            range.Source = vertex;
        }

        public bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            return range.Source != null
                && range.Source.IsIsolated()
                && range.CanBeInRange(vertex);
        }
    }
}