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
        private IReadOnlyCollection<IUndoCommand<TVertex>> UndoCommands { get; }

        private IReadOnlyCollection<IPathfindingRangeCommand<TVertex>> IncludeCommands { get; }

        private IReadOnlyCollection<IPathfindingRangeCommand<TVertex>> ExcludeCommands { get; }

        public IPathfindingRange<TVertex> Range { get; }

        public PathfindingRangeBuilder(IPathfindingRange<TVertex> range, 
            IReadOnlyCollection<IPathfindingRangeCommand<TVertex>> includeCommands,
            IReadOnlyCollection<IPathfindingRangeCommand<TVertex>> excludeCommands)
        {
            Range = range;
            IncludeCommands = includeCommands;
            ExcludeCommands = excludeCommands;
            UndoCommands = GetUndoCommands();
        }

        public void Include(TVertex vertex)
        {
            ExecuteFirst(IncludeCommands, vertex);
        }

        public void Exclude(TVertex vertex)
        {
            ExecuteFirst(ExcludeCommands, vertex);
        }

        private void ExecuteFirst(IReadOnlyCollection<IPathfindingRangeCommand<TVertex>> commands, TVertex vertex)
        {
            commands.FirstOrDefault(command => command.CanExecute(Range, vertex))
                ?.Execute(Range, vertex);
        }

        private IReadOnlyCollection<IUndoCommand<TVertex>> GetUndoCommands()
        {
            return IncludeCommands
                .Concat(ExcludeCommands)
                .OfType<IUndoCommand<TVertex>>()
                .ToReadOnly();
        }

        public void Undo()
        {
            UndoCommands.ForEach(command => command.Undo(Range));
        }
    }
}
