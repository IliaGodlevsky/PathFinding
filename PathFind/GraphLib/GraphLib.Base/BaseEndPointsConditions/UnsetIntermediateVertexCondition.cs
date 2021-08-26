using GraphLib.Interfaces;

namespace GraphLib.Base.BaseEndPointsConditions
{
    internal sealed class UnsetIntermediateVertexCondition
        : BaseEndPointsCondition, IEndPointsCondition
    {
        public UnsetIntermediateVertexCondition(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public bool IsTrue(IVertex vertex)
        {
            return endPoints.HasEndPointsSet
                && endPoints.IsIntermediate(vertex);
        }

        public void Execute(IVertex vertex)
        {
            endPoints.intermediates.Remove(vertex);
            (vertex as IVisualizable)?.VisualizeAsRegular();
        }
    }
}
