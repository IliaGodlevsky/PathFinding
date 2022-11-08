using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Visualization.Commands.Abstractions;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    [Order(0)]
    internal sealed class UnsetSourceVertexCommand<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public UnsetSourceVertexCommand(VisualPathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {

        }

        public override void Execute(TVertex vertex)
        {
            vertex?.VisualizeAsRegular();
            pathfindingRange.Source = default;
        }

        public override bool CanExecute(TVertex vertex)
        {
            return pathfindingRange.Source?.Equals(vertex) == true;
        }
    }
}