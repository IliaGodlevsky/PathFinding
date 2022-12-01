using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Shared.Executable;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Modules
{
    public sealed class PathfindingRangeBuilder<TVertex> : IPathfindingRangeBuilder<TVertex>, IUndo
        where TVertex : IVertex
    {
        private readonly Lazy<IReadOnlyCollection<IUndoCommand<TVertex>>> undoCommands;

        private IReadOnlyCollection<IUndoCommand<TVertex>> UndoCommands => undoCommands.Value;

        public IReadOnlyCollection<IPathfindingRangeCommand<TVertex>> IncludeCommands { get; set; }

        public IReadOnlyCollection<IPathfindingRangeCommand<TVertex>> ExcludeCommands { get; set; }

        public IPathfindingRange<TVertex> Range { get; }

        public PathfindingRangeBuilder(IPathfindingRange<TVertex> range)
        {
            Range = range;
            undoCommands = new Lazy<IReadOnlyCollection<IUndoCommand<TVertex>>>(GetUndoCommands);
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
            commands.FirstOrDefault(command => command.CanExecute(Range, vertex))?.Execute(Range, vertex);
        }

        private IReadOnlyCollection<IUndoCommand<TVertex>> GetUndoCommands()
        {
            return IncludeCommands.Concat(ExcludeCommands).OfType<IUndoCommand<TVertex>>().ToReadOnly();
        }

        public void Undo()
        {
            UndoCommands.ForEach(command => command.Undo(Range));
        }
    }
}
