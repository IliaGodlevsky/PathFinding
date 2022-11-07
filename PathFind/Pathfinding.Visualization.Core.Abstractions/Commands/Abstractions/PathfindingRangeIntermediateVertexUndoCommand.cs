using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Collections.ObjectModel;

namespace Pathfinding.Visualization.Core.Abstractions.Commands.Abstractions
{
    internal abstract class PathfindingRangeIntermediateVertexUndoCommand<TVertex> : PathfindingRangeUndoCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        protected Collection<TVertex> Intermediates => pathfindingRange.IntermediateVertices;

        protected Collection<TVertex> MarkedToReplace => pathfindingRange.MarkedToRemoveIntermediateVertices;

        protected PathfindingRangeIntermediateVertexUndoCommand(VisualPathfindingRange<TVertex> endPoints) : base(endPoints)
        {

        }
    }
}