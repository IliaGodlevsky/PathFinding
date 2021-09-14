using Common.Extensions;
using GraphLib.Base.EndPointsCondition.Interface;
using GraphLib.Base.EndPointsInspection.Abstractions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPointsCondition.Realizations.MiddleButtonConditions
{
    internal sealed class CancelMarkToReplaceEndPointsConditions
        : BaseIntermediateEndPointsInspection, IEndPointsCondition
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
