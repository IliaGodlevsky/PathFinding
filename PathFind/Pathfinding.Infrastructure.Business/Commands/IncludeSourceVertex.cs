using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Shared;

namespace Pathfinding.Infrastructure.Business.Commands
{
    [Order(2)]
    public sealed class IncludeSourceVertex<TVertex> : IPathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        public void Execute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            range.Source = vertex;
        }

        public bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            return range.Source == null
                && range.CanBeInRange(vertex);
        }
    }
}