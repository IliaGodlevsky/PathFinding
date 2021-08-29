using GraphLib.Base.EndPointsCondition.Interface;
using GraphLib.Base.EndPointsInspection.Abstractions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;

namespace GraphLib.Base.EndPointsCondition.Realizations
{
    internal sealed class SetTargetVertexCondition
        : BaseEndPointsInspection, IEndPointsCondition
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
