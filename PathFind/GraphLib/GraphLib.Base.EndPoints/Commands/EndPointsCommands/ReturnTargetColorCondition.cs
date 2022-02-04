using GraphLib.Base.EndPoints.EndPointsInspection;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    internal sealed class ReturnTargetColorCondition : BaseEndPointsInspection, IVertexCommand
    {
        public ReturnTargetColorCondition(BaseEndPoints endPoints) : base(endPoints)
        {
        }

        public void Execute(IVertex vertex)
        {
            (vertex as IVisualizable)?.VisualizeAsTarget();
        }

        public bool IsTrue(IVertex vertex)
        {
            return vertex.Equals(endPoints.Target);
        }
    }
}