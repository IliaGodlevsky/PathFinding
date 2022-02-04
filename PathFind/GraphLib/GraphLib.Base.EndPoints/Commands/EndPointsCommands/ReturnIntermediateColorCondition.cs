using GraphLib.Base.EndPoints.EndPointsInspection;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    internal sealed class ReturnIntermediateColorCondition : BaseEndPointsInspection, IVertexCommand
    {
        public ReturnIntermediateColorCondition(BaseEndPoints endPoints) : base(endPoints)
        {
        }

        public void Execute(IVertex vertex)
        {
            (vertex as IVisualizable)?.VisualizeAsIntermediate();
        }

        public bool IsTrue(IVertex vertex)
        {
            return endPoints.intermediates.Contains(vertex);
        }
    }
}