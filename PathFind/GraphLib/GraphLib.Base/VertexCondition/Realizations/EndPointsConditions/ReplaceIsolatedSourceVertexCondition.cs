using GraphLib.Base.EndPointsInspection.Abstractions;
using GraphLib.Base.VertexCondition.Interface;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.VertexCondition.Realizations.EndPointsConditions
{
    internal sealed class ReplaceIsolatedSourceVertexCondition
        : BaseEndPointsInspection, IVertexCondition
    {
        public ReplaceIsolatedSourceVertexCondition(BaseEndPoints endPoints)
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
