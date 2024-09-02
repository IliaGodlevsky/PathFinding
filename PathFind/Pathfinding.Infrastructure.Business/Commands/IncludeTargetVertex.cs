using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Shared;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Commands
{
    [Order(4), Group(Constants.IncludeCommands)]
    public sealed class IncludeTargetVertex<TVertex> : IPathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        private readonly IPathfindingRangeCommand<TVertex> undoCommand;

        public IncludeTargetVertex()
        {
            undoCommand = new ExcludeTargetVertex<TVertex>();
        }

        public void Execute(IList<TVertex> range, TVertex vertex)
        {
            range.Add(vertex);
        }

        public bool CanExecute(IList<TVertex> range, TVertex vertex)
        {
            return range.Count == 1
                && range.CanBeInRange(vertex);
        }
    }
}
