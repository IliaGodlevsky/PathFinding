using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Shared;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Commands
{
    [Order(1), Group(Constants.IncludeCommands)]
    public sealed class ReplaceTransitIsolatedVertex<TVertex> : IPathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        public void Execute(IList<TVertex> range, TVertex vertex)
        {
            var isolated = range
                .Skip(1)
                .Take(range.Count - 2)
                .First(IsIsolated);
            int isolatedIndex = range.IndexOf(isolated);
            range.Remove(isolated);
            range.Insert(isolatedIndex, vertex);
        }

        public bool CanExecute(IList<TVertex> range, TVertex vertex)
        {
            return range.Count > 2
                && range.Skip(1).Take(range.Count - 2).Any(IsIsolated)
                && range.CanBeInRange(vertex);
        }

        private static bool IsIsolated(TVertex vertex)
        {
            return vertex.IsIsolated();
        }
    }
}