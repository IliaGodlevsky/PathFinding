using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Visualization.Commands.Abstractions;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    [Order(5)]
    internal sealed class ReplaceIsolatedSourceVertexCommand<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public ReplaceIsolatedSourceVertexCommand(VisualPathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        public override void Execute(TVertex vertex)
        {
            Source.VisualizeAsRegular();
            pathfindingRange.Source = vertex;
            Source.VisualizeAsSource();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return pathfindingRange.Source.IsIsolated()
                && pathfindingRange.Source != null
                && pathfindingRange.CanBeEndPoint(vertex);
        }
    }
}