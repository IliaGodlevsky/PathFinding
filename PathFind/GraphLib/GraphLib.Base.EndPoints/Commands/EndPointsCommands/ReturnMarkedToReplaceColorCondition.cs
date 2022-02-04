using GraphLib.Base.EndPoints.EndPointsInspection;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    internal sealed class ReturnMarkedToReplaceColorCondition : BaseEndPointsInspection, IVertexCommand
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
