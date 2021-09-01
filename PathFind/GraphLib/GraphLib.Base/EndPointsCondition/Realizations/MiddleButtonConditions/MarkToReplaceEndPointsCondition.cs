using GraphLib.Base.EndPointsCondition.Interface;
using GraphLib.Base.EndPointsInspection.Abstractions;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPointsCondition.Realizations.MiddleButtonConditions
{
    internal sealed class MarkToReplaceEndPointsCondition
        : BaseIntermediateEndPointsInspection, IEndPointsCondition
    {
        public MarkToReplaceEndPointsCondition(BaseEndPoints endPoints) : base(endPoints)
        {
        }

        public void Execute(IVertex vertex)
        {
            endPoints.markedToReplaceIntermediates.Enqueue(vertex);
            (vertex as IVisualizable)?.VisualizeAsMarkedToReplaceIntermediate();
        }

        public bool IsTrue(IVertex vertex)
        {
            return !vertex.IsOneOf(endPoints.Source, endPoints.Target)
                && IsIntermediate(vertex)
                && !endPoints.markedToReplaceIntermediates.Contains(vertex);
        }
    }
}
