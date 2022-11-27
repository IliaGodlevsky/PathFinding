using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Range.Commands.Abstractions;
using Pathfinding.GraphLib.Core.Realizations.Range;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Primitives.Attributes;

namespace Pathfinding.GraphLib.Visualization.Commands.Realizations.PathfindingRangeCommands
{
    [Order(0)]
    internal sealed class RestoreSourceVisual<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public RestoreSourceVisual(PathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {
        }

        public override void Execute(TVertex vertex)
        {
            vertex.VisualizeAsSource();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return vertex.Equals(pathfindingRange.Source);
        }
    }
}