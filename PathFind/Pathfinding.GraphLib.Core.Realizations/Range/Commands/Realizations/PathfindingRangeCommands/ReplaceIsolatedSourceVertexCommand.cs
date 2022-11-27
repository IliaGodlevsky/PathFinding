using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Range.Commands.Abstractions;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Core.Realizations.Range.Commands.Realizations.PathfindingRangeCommands
{
    [Order(5)]
    internal sealed class ReplaceIsolatedSourceVertex<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        public ReplaceIsolatedSourceVertex(PathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        public override void Execute(TVertex vertex)
        {
            pathfindingRange.Source = vertex;
        }

        public override bool CanExecute(TVertex vertex)
        {
            return pathfindingRange.Source != null
                && pathfindingRange.Source.IsIsolated()
                && pathfindingRange.CanBeInRange(vertex);
        }
    }
}