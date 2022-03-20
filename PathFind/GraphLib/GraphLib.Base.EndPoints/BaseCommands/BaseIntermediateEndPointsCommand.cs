using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;

namespace GraphLib.Base.EndPoints.BaseCommands
{
    internal abstract class BaseIntermediateEndPointsCommand : BaseEndPointsCommand
    {
        protected Collection<IVertex> Intermediates => endPoints.Intermediates;

        protected Collection<IVertex> MarkedToReplace => endPoints.MarkedToReplace;

        protected bool HasIsolatedIntermediates => HasIsolated(Intermediates);

        protected BaseIntermediateEndPointsCommand(BaseEndPoints endPoints)
            : base(endPoints)
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