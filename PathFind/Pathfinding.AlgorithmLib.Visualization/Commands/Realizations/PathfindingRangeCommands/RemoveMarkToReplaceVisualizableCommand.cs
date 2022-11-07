using Pathfinding.AlgorithmLib.Visualization.Commands.Abstractions;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Primitives.Attributes;

namespace Pathfinding.AlgorithmLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    [Order(0)]
    internal sealed class RemoveMarkToReplaceVisualizableCommand<TVertex> : PathfindingRangeIntermediateVertexCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public RemoveMarkToReplaceVisualizableCommand(VisualPathfindingRange<TVertex> endPoints)
            : base(endPoints)
        {

        }

        public override void Execute(TVertex vertex)
        {
            MarkedToRemoveIntermediates.Remove(vertex);
            vertex.VisualizeAsIntermediate();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return IsMarkedToReplace(vertex);
        }
    }
}
