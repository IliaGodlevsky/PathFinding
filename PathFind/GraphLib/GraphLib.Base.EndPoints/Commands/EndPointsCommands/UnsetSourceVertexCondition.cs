using GraphLib.Base.EndPoints;
using GraphLib.Base.EndPoints.EndPointsInspection;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;

namespace GraphLib.Base.VertexCondition.EndPointsConditions
{
    internal sealed class UnsetSourceVertexCondition
        : BaseEndPointsInspection, IVertexCommand
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
            endPoints.Source = NullVertex.Instance;
        }
    }
}
