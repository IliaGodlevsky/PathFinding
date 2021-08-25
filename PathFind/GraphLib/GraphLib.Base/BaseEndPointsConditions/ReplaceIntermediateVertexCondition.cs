using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Linq;

namespace GraphLib.Base.BaseEndPointsConditions
{
    internal sealed class ReplaceIntermediateVertexCondition
        : BaseEndPointsCondition, IEndPointsCondition
    {
        public ReplaceIntermediateVertexCondition(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public bool IsTrue(IVertex vertex)
        {
            return endPoints.HasEndPointsSet
                && !endPoints.IsIntermediate(vertex)
                && endPoints.HasIsolatedIntermediates;
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
