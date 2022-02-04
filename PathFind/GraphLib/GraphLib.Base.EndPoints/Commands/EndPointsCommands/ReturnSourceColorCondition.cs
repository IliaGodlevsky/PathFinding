using GraphLib.Base.EndPoints.EndPointsInspection;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    internal sealed class ReturnSourceColorCondition : BaseEndPointsInspection, IVertexCommand
    {
        public ReturnSourceColorCondition(BaseEndPoints endPoints) : base(endPoints)
        {
        }

        public void Execute(IVertex vertex)
        {
            (vertex as IVisualizable)?.VisualizeAsSource();
        }

        public bool IsTrue(IVertex vertex)
        {
            return vertex.Equals(endPoints.Source);
        }
    }
}
