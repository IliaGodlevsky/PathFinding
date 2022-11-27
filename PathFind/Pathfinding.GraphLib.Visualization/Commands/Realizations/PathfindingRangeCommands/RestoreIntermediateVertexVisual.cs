using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Range.Commands.Abstractions;
using Pathfinding.GraphLib.Core.Realizations.Range;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    [Order(2)]
    internal sealed class RestoreIntermediateVertexVisual<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public RestoreIntermediateVertexVisual(PathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        public override void Execute(TVertex vertex)
        {
            vertex.VisualizeAsIntermediate();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return IntermediateVertices.Contains(vertex);
        }
    }
}