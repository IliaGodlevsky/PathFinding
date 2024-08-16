using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Commands
{
    public sealed class PathfindingRangeBuilder<TVertex> : IPathfindingRangeBuilder<TVertex>
        where TVertex : IVertex
    {
        private IReadOnlyCollection<IUndoCommand<TVertex>> UndoCommands { get; }

        private IReadOnlyCollection<IPathfindingRangeCommand<TVertex>> IncludeCommands { get; }

        private IReadOnlyCollection<IPathfindingRangeCommand<TVertex>> ExcludeCommands { get; }

        public IPathfindingRange<TVertex> Range { get; }

        public PathfindingRangeBuilder(IPathfindingRange<TVertex> range,
            IReadOnlyCollection<IPathfindingRangeCommand<TVertex>> commands)
        {
            Range = range;
            var groupedCommands = commands.GroupBy(x => x.GetGroupToken())
                .ToDictionary(x => x.Key, x => x.OrderByOrderAttribute().ToList());
            IncludeCommands = groupedCommands.GetOrEmpty(Constants.IncludeCommands).ToReadOnly();
            ExcludeCommands = groupedCommands.GetOrEmpty(Constants.ExcludeCommands).ToReadOnly();
            UndoCommands = commands.OfType<IUndoCommand<TVertex>>().ToReadOnly();
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
