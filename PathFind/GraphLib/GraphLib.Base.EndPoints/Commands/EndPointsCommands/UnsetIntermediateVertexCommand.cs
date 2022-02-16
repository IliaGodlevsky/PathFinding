using Common.Attrbiutes;
using Common.Extensions.EnumerableExtensions;
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
            if (IsMarkedToReplace(vertex))
            {
                endPoints.markedToReplaceIntermediates.Remove(vertex);
            }
            endPoints.intermediates.Remove(vertex);
            (vertex as IVisualizable)?.VisualizeAsRegular();
        }
    }
}
