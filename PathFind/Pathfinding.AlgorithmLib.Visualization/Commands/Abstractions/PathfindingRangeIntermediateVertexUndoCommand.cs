using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;

namespace Pathfinding.AlgorithmLib.Visualization.Commands.Abstractions
{
    internal abstract class PathfindingRangeIntermediateVertexUndoCommand<TVertex> : PathfindingRangeUndoCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        protected PathfindingRangeIntermediateVertexUndoCommand(VisualPathfindingRange<TVertex> endPoints) : base(endPoints)
        {

        }
    }
}