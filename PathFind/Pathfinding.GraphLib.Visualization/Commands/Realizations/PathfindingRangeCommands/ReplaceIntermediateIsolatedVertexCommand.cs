using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Visualization.Commands.Abstractions;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.Visualization.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Primitives.Attributes;
using System.Linq;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    [Order(3)]
    internal sealed class ReplaceIntermediateIsolatedVertexCommand<TVertex> : PathfindingRangeIntermediateVertexCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public ReplaceIntermediateIsolatedVertexCommand(PathfindingRangeAdapter<TVertex> endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(TVertex vertex)
        {
            var isolated = IntermediateVertices.First(v => v.IsIsolated());
            int isolatedIndex = IntermediateVertices.IndexOf(isolated);
            IntermediateVertices.Remove(isolated);
            MarkedToRemoveIntermediates.Remove(isolated);
            isolated.VisualizeAsRegular();
            IntermediateVertices.Insert(isolatedIndex, vertex);
            vertex.VisualizeAsIntermediate();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return adapter.HasSourceAndTargetSet()
                && HasIsolatedIntermediates
                && adapter.CanBeInRange(vertex);
        }
    }
}