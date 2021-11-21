using GraphLib.Base.EndPointsInspection.Abstractions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Linq;

namespace GraphLib.Base.VertexCondition.EndPointsConditions
{
    internal sealed class ReplaceIntermediateIsolatedVertexCondition
        : BaseIntermediateEndPointsInspection, IVertexCondition
    {
        public ReplaceIntermediateIsolatedVertexCondition(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public bool IsTrue(IVertex vertex)
        {
            return endPoints.HasSourceAndTargetSet()
                && !IsIntermediate(vertex)
                && HasIsolatedIntermediates
                && !endPoints.IsEndPoint(vertex);
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
