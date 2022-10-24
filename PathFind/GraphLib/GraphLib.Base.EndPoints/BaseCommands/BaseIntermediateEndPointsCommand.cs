using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;

namespace GraphLib.Base.EndPoints.BaseCommands
{
    internal abstract class BaseIntermediatePathfindingRangeCommand : BasePathfindingRangeCommand
    {
        protected Collection<IVertex> Intermediates => range.Intermediates;

        protected Collection<IVertex> MarkedToReplace => range.MarkedToReplace;

        protected bool HasIsolatedIntermediates => HasIsolated(Intermediates);

        protected BaseIntermediatePathfindingRangeCommand(BasePathfindingRange range)
            : base(range)
        {

        }

        protected bool IsIntermediate(IVertex vertex)
        {
            return Intermediates.Contains(vertex);
        }

        protected bool IsMarkedToReplace(IVertex vertex)
        {
            return MarkedToReplace.Contains(vertex);
        }

        private static bool HasIsolated(Collection<IVertex> collection)
        {
            return collection.Count > 0 && collection.Any(vertex => vertex.IsIsolated());
        }
    }
}