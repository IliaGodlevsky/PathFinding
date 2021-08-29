using GraphLib.Base.EndPointsCondition.Interface;
using GraphLib.Base.EndPointsInspection.Abstractions;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPointsCondition.Realizations
{
    internal sealed class ReplaceSourceVertexCondition
        : BaseEndPointsInspection, IEndPointsCondition
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
