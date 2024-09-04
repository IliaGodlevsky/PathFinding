using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Shared;

namespace Pathfinding.Infrastructure.Business.Commands
{
    [Order(4), Group(Constants.IncludeCommands)]
    public sealed class IncludeTargetVertex<TVertex> : IPathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        public void Execute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            range.Target = vertex;
        }

        public bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            return range.Source != null
                && range.Target == null
                && range.CanBeInRange(vertex);
        }
    }
}
