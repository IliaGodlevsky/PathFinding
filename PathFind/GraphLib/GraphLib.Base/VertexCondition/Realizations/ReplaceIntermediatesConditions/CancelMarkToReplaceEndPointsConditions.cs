using Common.Extensions;
using GraphLib.Base.EndPointsInspection.Abstractions;
using GraphLib.Base.VertexCondition.Interface;
using GraphLib.Interfaces;

namespace GraphLib.Base.VertexCondition.Realizations.ReplaceIntermediatesConditions
{
    internal sealed class CancelMarkToReplaceEndPointsConditions
        : BaseIntermediateEndPointsInspection, IVertexCondition
    {
        public CancelMarkToReplaceEndPointsConditions(BaseEndPoints endPoints) : base(endPoints)
        {

        }

        public void Execute(IVertex vertex)
        {
            endPoints.markedToReplaceIntermediates.Remove(vertex);
            (vertex as IVisualizable)?.VisualizeAsIntermediate();
        }

        public bool IsTrue(IVertex vertex)
        {
            return endPoints.markedToReplaceIntermediates.Contains(vertex);
        }
    }
}
