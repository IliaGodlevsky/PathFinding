using Common.Extensions.EnumerableExtensions;
using GraphLib.Base.EndPoints.EndPointsInspection;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.ReplaceIntermediatesConditions
{
    internal sealed class CancelMarkToReplaceEndPointsConditions
        : BaseIntermediateEndPointsInspection, IVertexCommand
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
