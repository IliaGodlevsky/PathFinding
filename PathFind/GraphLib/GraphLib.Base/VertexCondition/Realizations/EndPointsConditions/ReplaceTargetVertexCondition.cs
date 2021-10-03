using GraphLib.Base.EndPointsInspection.Abstractions;
using GraphLib.Base.VertexCondition.Interface;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.VertexCondition.Realizations.EndPointsConditions
{
    internal sealed class ReplaceIsolatedTargetVertexCondition
        : BaseEndPointsInspection, IVertexCondition
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
