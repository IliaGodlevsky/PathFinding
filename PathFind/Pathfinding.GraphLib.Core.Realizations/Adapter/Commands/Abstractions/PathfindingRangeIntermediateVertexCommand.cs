using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Realizations.Adapter.Commands.Abstractions
{
    public abstract class PathfindingRangeIntermediateVertexCommand<TVertex> : PathfindingRangeCommand<TVertex>
        where TVertex : IVertex
    {
        protected bool HasIsolatedIntermediates => HasIsolated(IntermediateVertices);

        protected PathfindingRangeIntermediateVertexCommand(PathfindingRange<TVertex> adapter)
            : base(adapter)
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