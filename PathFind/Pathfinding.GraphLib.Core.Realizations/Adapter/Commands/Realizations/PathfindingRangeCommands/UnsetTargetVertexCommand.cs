using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Adapter;
using Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Abstractions;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Realizations.PathfindingRangeCommands
{
    [Order(1)]
    internal sealed class UnsetTargetVertexCommand<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        public UnsetTargetVertexCommand(PathfindingRange<TVertex> endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(TVertex vertex)
        {
            adapter.Target = default;
        }

        public override bool CanExecute(TVertex vertex)
        {
            return adapter.Target?.Equals(vertex) == true;
        }
    }
}