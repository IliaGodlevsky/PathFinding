using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Abstractions;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Realizations.PathfindingRangeCommands
{
    [Order(0)]
    internal sealed class UnsetSourceVertexCommand<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        public UnsetSourceVertexCommand(PathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {

        }

        public override void Execute(TVertex vertex)
        {
            adapter.Source = default;
        }

        public override bool CanExecute(TVertex vertex)
        {
            return adapter.Source?.Equals(vertex) == true;
        }
    }
}