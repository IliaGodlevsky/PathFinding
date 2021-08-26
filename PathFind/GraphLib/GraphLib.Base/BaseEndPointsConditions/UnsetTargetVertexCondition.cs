using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;

namespace GraphLib.Base.BaseEndPointsConditions
{
    internal sealed class UnsetTargetVertexCondition
        : BaseEndPointsCondition, IEndPointsCondition
    {
        public UnsetTargetVertexCondition(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public bool IsTrue(IVertex vertex)
        {
            return endPoints.Target.IsEqual(vertex);
        }

        public void Execute(IVertex vertex)
        {
            (vertex as IVisualizable)?.VisualizeAsRegular();
            endPoints.Target = new NullVertex();
        }
    }
}
