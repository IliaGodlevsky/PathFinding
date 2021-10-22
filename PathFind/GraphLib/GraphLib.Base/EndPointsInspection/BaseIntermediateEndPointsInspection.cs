using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Linq;

namespace GraphLib.Base.EndPointsInspection.Abstractions
{
    internal abstract class BaseIntermediateEndPointsInspection : BaseEndPointsInspection
    {
        protected BaseIntermediateEndPointsInspection(BaseEndPoints endPoints) : base(endPoints)
        {

        }

        protected bool HasIsolatedIntermediates => endPoints.intermediates.Any(vertex => vertex.IsIsolated());

        protected bool IsIntermediate(IVertex vertex)
        {
            return endPoints.intermediates.Contains(vertex);
        }
    }
}
