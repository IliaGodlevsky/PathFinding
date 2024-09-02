using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Shared;
using System.Collections.Generic;

namespace Pathfinding.Infrastructure.Business.Commands
{
    [Order(2), Group(Constants.IncludeCommands)]
    public sealed class IncludeSourceVertex<TVertex> : IPathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        private readonly IPathfindingRangeCommand<TVertex> excludeCommand;

        public IncludeSourceVertex()
        {
            excludeCommand = new ExcludeSourceVertex<TVertex>();
        }

        public void Execute(IList<TVertex> range, TVertex vertex)
        {
            range.Insert(0, vertex);
        }

        public bool CanExecute(IList<TVertex> range, TVertex vertex)
        {
            return range.Count == 0 &&
                !range.Contains(vertex)
                && range.CanBeInRange(vertex);
        }
    }
}