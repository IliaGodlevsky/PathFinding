using GraphLib.Base.EndPointsInspection.Abstractions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;

namespace GraphLib.Base.VertexCondition.EndPointsConditions
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
            return endPoints.Source.IsIsolated()
                && !endPoints.Source.IsNull()
                && !endPoints.IsEndPoint(vertex);
        }
    }
}
