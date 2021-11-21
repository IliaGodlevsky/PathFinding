using GraphLib.Base.EndPointsInspection.Abstractions;
using GraphLib.Interfaces;

namespace GraphLib.Base.VertexCondition.EndPointsConditions
{
    internal sealed class UnsetIntermediateVertexCondition
        : BaseIntermediateEndPointsInspection, IVertexCondition
    {
        public UnsetIntermediateVertexCondition(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public bool IsTrue(IVertex vertex)
        {
            return IsIntermediate(vertex);
        }

        public void Execute(IVertex vertex)
        {
            endPoints.intermediates.Remove(vertex);
            (vertex as IVisualizable)?.VisualizeAsRegular();
        }
    }
}
