using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.Visualization.Core.Abstractions.Commands.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Primitives.Attributes;

namespace Pathfinding.Visualization.Core.Abstractions.Commands.Realizations.PathfindingRangeCommands
{
    [Order(0)]
    internal sealed class RemoveMarkToReplaceVisualizableCommand<TVertex> : PathfindingRangeIntermediateVertexCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public RemoveMarkToReplaceVisualizableCommand(VisualPathfindingRange<TVertex> endPoints)
            : base(endPoints)
        {

        }

        public override void Execute(IVisualizable vertex)
        {
            MarkedToReplace.Remove(vertex);
            vertex.VisualizeAsIntermediate();
        }

        public override bool CanExecute(IVisualizable vertex)
        {
            return IsMarkedToReplace(vertex);
        }
    }
}
