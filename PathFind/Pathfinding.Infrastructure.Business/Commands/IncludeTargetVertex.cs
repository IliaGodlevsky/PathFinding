﻿using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Shared;

namespace Pathfinding.Infrastructure.Business.Commands
{
    [Order(4), Group(Constants.IncludeCommands)]
    public sealed class IncludeTargetVertex<TVertex> : IPathfindingRangeCommand<TVertex>, IUndoCommand<TVertex>
        where TVertex : IVertex
    {
        private readonly IPathfindingRangeCommand<TVertex> undoCommand;

        public IncludeTargetVertex()
        {
            undoCommand = new ExcludeTargetVertex<TVertex>();
        }

        public void Execute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            range.Target = vertex;
        }

        public bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            return range.Source != null
                && range.Target == null
                && range.CanBeInRange(vertex);
        }

        public void Undo(IPathfindingRange<TVertex> range)
        {
            undoCommand.Execute(range, range.Target);
        }
    }
}
