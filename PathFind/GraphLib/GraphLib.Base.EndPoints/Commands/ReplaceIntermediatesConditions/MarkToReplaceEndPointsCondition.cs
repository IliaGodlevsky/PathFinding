using GraphLib.Base.EndPoints.EndPointsInspection;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.ReplaceIntermediatesConditions
{
    internal sealed class MarkToReplaceEndPointsCondition
        : BaseIntermediateEndPointsInspection, IVertexCommand
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
