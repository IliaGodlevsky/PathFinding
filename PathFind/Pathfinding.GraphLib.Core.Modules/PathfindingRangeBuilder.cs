using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Shared.Executable;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Modules
{
    public sealed class PathfindingRangeBuilder<TVertex> : IPathfindingRangeBuilder<TVertex>, IUndo
        where TVertex : IVertex
    {
        private IReadOnlyCollection<IUndoCommand<TVertex>> undoCommands;
        private IReadOnlyCollection<IPathfindingRangeCommand<TVertex>> includeCommands;
        private IReadOnlyCollection<IPathfindingRangeCommand<TVertex>> excludeCommands;

        public IPathfindingRange<TVertex> Range { get; }

        public PathfindingRangeBuilder(IPathfindingRange<TVertex> range,
            IReadOnlyCollection<IPathfindingRangeCommand<TVertex>> includeCommands,
            IReadOnlyCollection<IPathfindingRangeCommand<TVertex>> excludeCommands)
        {
            Range = range;
            this.includeCommands = includeCommands;
            this.excludeCommands = excludeCommands;
            undoCommands = GetUndoCommands();
        }

        public void Include(TVertex vertex)
        {
            ExecuteFirst(includeCommands, vertex);
        }

        public void Exclude(TVertex vertex)
        {
            ExecuteFirst(excludeCommands, vertex);
        }

        private void ExecuteFirst(IReadOnlyCollection<IPathfindingRangeCommand<TVertex>> commands, TVertex vertex)
        {
            commands.FirstOrDefault(command => command.CanExecute(Range, vertex))
                ?.Execute(Range, vertex);
        }

        private IReadOnlyCollection<IUndoCommand<TVertex>> GetUndoCommands()
        {
            return includeCommands
                .Concat(excludeCommands)
                .OfType<IUndoCommand<TVertex>>()
                .ToReadOnly();
        }

        public void Undo()
        {
            undoCommands.ForEach(command => command.Undo(Range));
        }
    }
}
