using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.BaseEndPointsConditions
{
    internal sealed class ReplaceTargetVertexCondition
        : BaseEndPointsCondition, IEndPointsCondition
    {
        public ReplaceTargetVertexCondition(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public void Execute(IVertex vertex)
        {
            (endPoints.Target as IVisualizable)?.VisualizeAsRegular();
            endPoints.Target = vertex;
            (vertex as IVisualizable)?.VisualizeAsTarget();
        }

        public bool IsTrue(IVertex vertex)
        {
            return endPoints.Target.IsIsolated();
        }
    }
}
