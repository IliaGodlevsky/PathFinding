using GraphLib.Interfaces;
using NullObject.Extensions;

namespace GraphLib.Base.BaseEndPointsConditions
{
    internal sealed class SetTargetVertexCondition
        : BaseEndPointsCondition, IEndPointsCondition
    {
        public SetTargetVertexCondition(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public void Execute(IVertex vertex)
        {
            endPoints.Target = vertex;
            (vertex as IVisualizable)?.VisualizeAsTarget();
        }

        public bool IsTrue(IVertex vertex)
        {
            return !endPoints.Source.IsNull()
                && endPoints.Target.IsNull()
                && endPoints.CanBeEndPoint(vertex);
        }
    }
}
