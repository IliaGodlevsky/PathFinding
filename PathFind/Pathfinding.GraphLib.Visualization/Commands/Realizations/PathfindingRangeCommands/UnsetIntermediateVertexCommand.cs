using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Visualization.Commands.Abstractions;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    [Order(2)]
    internal sealed class UnsetIntermediateVertexCommand<TVertex> : PathfindingRangeIntermediateVertexCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public UnsetIntermediateVertexCommand(VisualPathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        public override void Execute(TVertex vertex)
        {
            if (IsMarkedToReplace(vertex))
            {
                MarkedToRemoveIntermediates.Remove(vertex);
            }
            IntermediateVertices.Remove(vertex);
            vertex.VisualizeAsRegular();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return IsIntermediate(vertex);
        }
    }
}