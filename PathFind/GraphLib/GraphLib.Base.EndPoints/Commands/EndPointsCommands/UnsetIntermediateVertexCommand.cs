using Common.Attrbiutes;
using GraphLib.Base.EndPoints.Attributes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Attachment(typeof(SetEndPointsCommands)), Order(2)]
    internal sealed class UnsetIntermediateVertexCommand : BaseIntermediateEndPointsCommand
    {
        public UnsetIntermediateVertexCommand(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public override bool IsTrue(IVertex vertex)
        {
            return IsIntermediate(vertex);
        }

        public override void Execute(IVertex vertex)
        {
            endPoints.intermediates.Remove(vertex);
            (vertex as IVisualizable)?.VisualizeAsRegular();
        }
    }
}
