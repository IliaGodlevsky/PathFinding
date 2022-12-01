using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;

namespace Pathfinding.GraphLib.Core.Modules.Commands
{
    public sealed class ReplaceIsolatedTargetVertex<TVertex> : IPathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        public void Execute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            range.Target = vertex;
        }

        public bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            return range.Target != null
                && range.Target.IsIsolated()
                && range.CanBeInRange(vertex);
        }
    }
}