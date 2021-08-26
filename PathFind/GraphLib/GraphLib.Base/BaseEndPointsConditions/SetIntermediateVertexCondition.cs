using GraphLib.Interfaces;

namespace GraphLib.Base.BaseEndPointsConditions
{
    internal sealed class SetIntermediateVertexCondition
        : BaseEndPointsCondition, IEndPointsCondition
    {
        public SetIntermediateVertexCondition(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public void Execute(IVertex vertex)
        {
            endPoints.intermediates.Add(vertex);
            (vertex as IVisualizable)?.VisualizeAsIntermediate();
        }

        public bool IsTrue(IVertex vertex)
        {
            return endPoints.HasEndPointsSet;
        }
    }
}
