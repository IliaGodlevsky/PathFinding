using GraphLib.Base.EndPoints.EndPointsInspection;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    internal sealed class ReplaceIsolatedTargetVertexCondition
        : BaseEndPointsInspection, IVertexCommand
    {
        public ReplaceIsolatedTargetVertexCondition(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public void Execute(IVertex vertex)
        {
            (endPoints.Target as IVisualizable)?.VisualizeAsRegular();
            endPoints.Target = vertex;
            (vertex as IVisualizable)?.VisualizeAsTarget();
        }

        public bool IsTrue(IVertex vertex)
        {
            return endPoints.Target.IsIsolated()
                && !endPoints.Target.IsNull()
                && !endPoints.IsEndPoint(vertex);
        }
    }
}
