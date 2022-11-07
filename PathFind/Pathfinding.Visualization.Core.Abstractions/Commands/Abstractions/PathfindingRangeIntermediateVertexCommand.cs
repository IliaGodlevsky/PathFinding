using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pathfinding.Visualization.Core.Abstractions.Commands.Abstractions
{
    internal abstract class PathfindingRangeIntermediateVertexCommand<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        protected Collection<IVisualizable> Intermediates => pathfindingRange.IntermediateVertices;

        protected Collection<TVertex> MarkedToReplace => pathfindingRange.MarkedToRemoveIntermediateVertices;

        protected bool HasIsolatedIntermediates => HasIsolated(Intermediates);

        protected PathfindingRangeIntermediateVertexCommand(VisualPathfindingRange<TVertex> pathfindingRange)
            : base(pathfindingRange)
        {

        }

        protected bool IsIntermediate(TVertex vertex)
        {
            return Intermediates.Contains(vertex);
        }

        protected bool IsMarkedToReplace(TVertex vertex)
        {
            return MarkedToReplace.Contains(vertex);
        }

        private static bool HasIsolated(IReadOnlyCollection<TVertex> collection)
        {
            return collection.Count > 0 && collection.Any(vertex => vertex.IsIsolated());
        }
    }
}