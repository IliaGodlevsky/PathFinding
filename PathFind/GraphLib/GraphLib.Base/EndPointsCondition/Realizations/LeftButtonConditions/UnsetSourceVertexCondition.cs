using GraphLib.Base.EndPointsCondition.Interface;
using GraphLib.Base.EndPointsInspection.Abstractions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;

namespace GraphLib.Base.EndPointsCondition.Realizations.LeftButtonConditions
{
    internal sealed class UnsetSourceVertexCondition
        : BaseEndPointsInspection, IEndPointsCondition
    {
        public UnsetSourceVertexCondition(BaseEndPoints endPoints)
            : base(endPoints)
        {

        }

        public bool IsTrue(IVertex vertex)
        {
            return vertex.IsEqual(endPoints.Source);
        }

        public void Execute(IVertex vertex)
        {
            (vertex as IVisualizable)?.VisualizeAsRegular();
            endPoints.Source = new NullVertex();
        }
    }
}
