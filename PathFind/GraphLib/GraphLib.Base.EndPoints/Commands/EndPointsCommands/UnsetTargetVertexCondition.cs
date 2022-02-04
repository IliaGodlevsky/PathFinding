using GraphLib.Base.EndPoints.EndPointsInspection;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    internal sealed class UnsetTargetVertexCondition
        : BaseEndPointsInspection, IVertexCommand
    {
        public UnsetTargetVertexCondition(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public bool IsTrue(IVertex vertex)
        {
            return endPoints.Target.IsEqual(vertex);
        }

        public void Execute(IVertex vertex)
        {
            (vertex as IVisualizable)?.VisualizeAsRegular();
            endPoints.Target = NullVertex.Instance;
        }
    }
}
