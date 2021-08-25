using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;

namespace GraphLib.Base.BaseEndPointsConditions
{
    internal sealed class UnserSourceVertexCondition
        : BaseEndPointsCondition, IEndPointsCondition
    {
        public UnserSourceVertexCondition(BaseEndPoints endPoints)
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
