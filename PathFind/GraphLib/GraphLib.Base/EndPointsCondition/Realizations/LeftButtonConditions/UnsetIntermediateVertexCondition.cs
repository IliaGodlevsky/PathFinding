using GraphLib.Base.EndPointsCondition.Interface;
using GraphLib.Base.EndPointsInspection.Abstractions;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPointsCondition.Realizations.LeftButtonConditions
{
    internal sealed class UnsetIntermediateVertexCondition
        : BaseIntermediateEndPointsInspection, IEndPointsCondition
    {
        public UnsetIntermediateVertexCondition(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public bool IsTrue(IVertex vertex)
        {
            return endPoints.HasSourceAndTargetSet()
                && IsIntermediate(vertex);
        }

        public void Execute(IVertex vertex)
        {
            endPoints.intermediates.Remove(vertex);
            (vertex as IVisualizable)?.VisualizeAsRegular();
        }
    }
}
