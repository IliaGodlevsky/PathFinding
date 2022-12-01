using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Modules.Interface;

namespace Pathfinding.GraphLib.Core.Modules.Commands
{
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