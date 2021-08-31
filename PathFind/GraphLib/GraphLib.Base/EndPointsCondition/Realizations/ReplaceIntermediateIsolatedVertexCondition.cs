using GraphLib.Base.EndPointsCondition.Interface;
using GraphLib.Base.EndPointsInspection.Abstractions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Linq;

namespace GraphLib.Base.EndPointsCondition.Realizations
{
    internal sealed class ReplaceIntermediateIsolatedVertexCondition
        : BaseIntermediateEndPointsInspection, IEndPointsCondition
    {
        public ReplaceIntermediateIsolatedVertexCondition(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public bool IsTrue(IVertex vertex)
        {
            return endPoints.HasSourceAndTargetSet()
                && !IsIntermediate(vertex)
                && HasIsolatedIntermediates;
        }

        public void Execute(IVertex vertex)
        {
            var isolated = endPoints.intermediates.FirstOrDefault(v => v.IsIsolated());
            if (!isolated.IsNull())
            {
                int isolatedIndex = endPoints.intermediates.IndexOf(isolated);
                endPoints.intermediates.Remove(isolated);
                (isolated as IVisualizable)?.VisualizeAsRegular();
                endPoints.intermediates.Insert(isolatedIndex, vertex);
                (vertex as IVisualizable)?.VisualizeAsIntermediate();
            }
        }
    }
}
