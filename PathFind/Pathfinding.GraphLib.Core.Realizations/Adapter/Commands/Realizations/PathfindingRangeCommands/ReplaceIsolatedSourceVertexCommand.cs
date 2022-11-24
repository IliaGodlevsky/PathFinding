using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Abstractions;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Realizations.PathfindingRangeCommands
{
    [Order(5)]
    internal sealed class ReplaceIsolatedSourceVertexCommand<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        public ReplaceIsolatedSourceVertexCommand(PathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        public override void Execute(TVertex vertex)
        {
            adapter.Source = vertex;
        }

        public override bool CanExecute(TVertex vertex)
        {
            return adapter.Source.IsIsolated()
                && adapter.Source != null
                && adapter.CanBeInRange(vertex);
        }
    }
}