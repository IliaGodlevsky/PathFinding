using GraphLib.Base.EndPointsInspection.Abstractions;
using GraphLib.Interfaces;

namespace GraphLib.Base.VertexCondition.EndPointsConditions
{
    internal sealed class ReturnMarkedToReplaceColorCondition : BaseEndPointsInspection, IVertexCondition
    {
        public ReturnMarkedToReplaceColorCondition(BaseEndPoints endPoints) : base(endPoints)
        {
        }

        public void Execute(IVertex vertex)
        {
            (vertex as IVisualizable)?.VisualizeAsIntermediate();
            (vertex as IVisualizable)?.VisualizeAsMarkedToReplaceIntermediate();
        }

        public bool IsTrue(IVertex vertex)
        {
            return endPoints.markedToReplaceIntermediates.Contains(vertex);
        }
    }
}
