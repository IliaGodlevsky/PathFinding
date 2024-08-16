using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Shared;

namespace Pathfinding.Infrastructure.Business.Commands
{
    [Order(2), Group(Constants.IncludeCommands)]
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