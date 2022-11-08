using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Visualization.Commands.Abstractions;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Primitives.Attributes;
using System.Linq;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    [Order(8)]
    internal sealed class ReplaceIntermediateVertexCommand<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public ReplaceIntermediateVertexCommand(VisualPathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {

        }

        public override void Execute(TVertex vertex)
        {
            var toRemove = MarkedToRemoveIntermediates.First();
            MarkedToRemoveIntermediates.Remove(toRemove);
            int toReplaceIndex = IntermediateVertices.IndexOf(toRemove);
            IntermediateVertices.Remove(toRemove);
            toRemove.VisualizeAsRegular();
            IntermediateVertices.Insert(toReplaceIndex, vertex);
            vertex.VisualizeAsIntermediate();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return MarkedToRemoveIntermediates.Count > 0
                && pathfindingRange.CanBeEndPoint(vertex);
        }
    }
}
