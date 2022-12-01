using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;

namespace Pathfinding.GraphLib.Core.Modules.Commands
{
    public sealed class IncludeSourceVertex<TVertex> : IPathfindingRangeCommand<TVertex>, IUndoCommand<TVertex>
        where TVertex : IVertex
    {
        private readonly IPathfindingRangeCommand<TVertex> excludeCommand;

        public IncludeSourceVertex()
        {
            excludeCommand = new ExcludeSourceVertex<TVertex>();
        }

        public void Execute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            range.Source = vertex;
        }

        public bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            return range.Source == null
                && range.CanBeInRange(vertex);
        }

        public void Undo(IPathfindingRange<TVertex> range)
        {
            excludeCommand.Execute(range, range.Source);
        }
    }
}