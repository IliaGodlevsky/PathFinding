using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.BaseEndPointsConditions
{
    internal sealed class ReplaceSourceVertexCondition
        : BaseEndPointsCondition, IEndPointsCondition
    {
        public ReplaceSourceVertexCondition(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public void Execute(IVertex vertex)
        {
            (endPoints.Source as IVisualizable)?.VisualizeAsRegular();
            endPoints.Source = vertex;
            (endPoints.Source as IVisualizable)?.VisualizeAsSource();
        }

        public bool IsTrue(IVertex vertex)
        {
            return endPoints.Source.IsIsolated();
        }
    }
}
