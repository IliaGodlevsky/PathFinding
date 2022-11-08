using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Visualization.Commands.Abstractions
{
    internal abstract class PathfindingRangeIntermediateVertexCommand<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        protected bool HasIsolatedIntermediates => HasIsolated(IntermediateVertices);

        protected PathfindingRangeIntermediateVertexCommand(VisualPathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {

        }

        protected bool IsIntermediate(TVertex vertex)
        {
            return IntermediateVertices.Contains(vertex);
        }

        protected bool IsMarkedToReplace(TVertex vertex)
        {
            return MarkedToRemoveIntermediates.Contains(vertex);
        }

        private static bool HasIsolated(ICollection<TVertex> collection)
        {
            return collection.Count > 0 && collection.Any(vertex => vertex.IsIsolated());
        }
    }
}