using GraphLib.Base.EndPointsCondition.Interface;
using GraphLib.Base.EndPointsInspection.Abstractions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;

namespace GraphLib.Base.EndPointsCondition.Realizations
{
    internal sealed class UnsetTargetVertexCondition
        : BaseEndPointsInspection, IEndPointsCondition
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
