using GraphLib.Base.EndPointsCondition.Interface;
using GraphLib.Base.EndPointsInspection.Abstractions;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPointsCondition.Realizations.LeftButtonConditions
{
    internal sealed class ReplaceIsolatedTargetVertexCondition
        : BaseEndPointsInspection, IEndPointsCondition
    {
        public ReplaceIsolatedTargetVertexCondition(BaseEndPoints endPoints)
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
