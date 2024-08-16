﻿using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Shared;

namespace Pathfinding.Infrastructure.Business.Commands
{
    [Order(6), Group(Constants.IncludeCommands)]
    public sealed class IncludeTransitVertex<TVertex> : IPathfindingRangeCommand<TVertex>, IUndoCommand<TVertex>
        where TVertex : IVertex
    {
        public void Execute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            range.Transit.Add(vertex);
        }

        public bool CanExecute(IPathfindingRange<TVertex> range, TVertex vertex)
        {
            return range.HasSourceAndTargetSet()
                && range.CanBeInRange(vertex);
        }

        public void Undo(IPathfindingRange<TVertex> range)
        {
            range.Transit.Clear();
        }
    }
}