using GraphLib.Base.EndPoints.EndPointsInspection;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    internal sealed class UnsetIntermediateVertexCondition
        : BaseIntermediateEndPointsInspection, IVertexCommand
    {
        public UnsetIntermediateVertexCondition(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public bool IsTrue(IVertex vertex)
        {
            return IsIntermediate(vertex);
        }

        public void Execute(IVertex vertex)
        {
            endPoints.intermediates.Remove(vertex);
            (vertex as IVisualizable)?.VisualizeAsRegular();
        }
    }
}
