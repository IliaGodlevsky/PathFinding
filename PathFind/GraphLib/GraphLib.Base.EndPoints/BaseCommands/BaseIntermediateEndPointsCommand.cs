using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;

namespace GraphLib.Base.EndPoints.BaseCommands
{
    internal abstract class BaseIntermediateEndPointsCommand : BaseEndPointsCommand
    {
        protected Collection<IVertex> Intermediates => endPoints.intermediates;
        protected Collection<IVertex> MarkedToReplace => endPoints.markedToReplaceIntermediates;

        protected IVisualizable LastIntermediate => Intermediates.LastOrDefault().AsVisualizable();
        protected IVisualizable LastMarkedToReplace => MarkedToReplace.LastOrDefault().AsVisualizable();

        protected bool AreThereMarkedToReplace => MarkedToReplace.Count > 0;
        protected bool HasIsolatedIntermediates => Intermediates.Any(vertex => vertex.IsIsolated());
        protected bool HasIsolatedMarkedToReplace => MarkedToReplace.Any(vertex => vertex.IsIsolated());

        protected BaseIntermediateEndPointsCommand(BaseEndPoints endPoints) : base(endPoints)
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


    }
}
