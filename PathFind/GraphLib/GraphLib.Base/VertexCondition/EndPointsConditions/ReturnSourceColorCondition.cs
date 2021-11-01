using GraphLib.Base.EndPointsInspection.Abstractions;
using GraphLib.Interfaces;

namespace GraphLib.Base.VertexCondition.EndPointsConditions
{
    internal sealed class ReturnSourceColorCondition : BaseEndPointsInspection, IVertexCondition
    {
        public ReturnSourceColorCondition(BaseEndPoints endPoints) : base(endPoints)
        {
        }

        public void Execute(IVertex vertex)
        {
            (vertex as IVisualizable)?.VisualizeAsSource();
        }

        public bool IsTrue(IVertex vertex)
        {
            return vertex.Equals(endPoints.Source);
        }
    }
}
