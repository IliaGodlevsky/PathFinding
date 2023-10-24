using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Modules
{
    public sealed class PathfindingRangeBuilder<TVertex> : IPathfindingRangeBuilder<TVertex>
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
            UndoCommands = IncludeCommands
                .Union(ExcludeCommands)
                .OfType<IUndoCommand<TVertex>>()
                .ToArray();
        }

        public void Include(TVertex vertex)
        {
            Execute(IncludeCommands, vertex);
        }

        public void Exclude(TVertex vertex)
        {
            Execute(ExcludeCommands, vertex);
        }

        public void Undo()
        {
            UndoCommands.ForEach(command => command.Undo(Range));
        }

        private void Execute(IEnumerable<IPathfindingRangeCommand<TVertex>> commands, TVertex vertex)
        {
            commands.FirstOrDefault(c => c.CanExecute(Range, vertex))?.Execute(Range, vertex);
        }
    }
}
