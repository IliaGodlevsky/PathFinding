using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Visualization.Commands.Abstractions;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    [Order(1)]
    internal sealed class UnsetTargetVertexCommand<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public UnsetTargetVertexCommand(VisualPathfindingRange<TVertex> endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(TVertex vertex)
        {
            vertex?.VisualizeAsRegular();
            pathfindingRange.Target = default;
        }

        public override bool CanExecute(TVertex vertex)
        {
            return pathfindingRange.Target?.Equals(vertex) == true;
        }
    }
}