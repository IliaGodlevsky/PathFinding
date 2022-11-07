using Pathfinding.AlgorithmLib.Visualization.Commands.Abstractions;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Extensions;
using Shared.Primitives.Attributes;

namespace Pathfinding.AlgorithmLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    [Order(1)]
    internal sealed class MarkToReplaceVisualizableCommand<TVertex> : PathfindingRangeIntermediateVertexCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public MarkToReplaceVisualizableCommand(VisualPathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        public override void Execute(TVertex vertex)
        {
            MarkedToRemoveIntermediates.Add(vertex);
            vertex.VisualizeAsMarkedToReplaceIntermediate();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return !vertex.IsOneOf(pathfindingRange.Source, pathfindingRange.Target)
                && IsIntermediate(vertex)
                && !IsMarkedToReplace(vertex);
        }
    }
}
