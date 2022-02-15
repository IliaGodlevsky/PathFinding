using Common.Attrbiutes;
using GraphLib.Base.EndPoints.Attributes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Attachment(typeof(SetEndPointsCommands)), Order(8)]
    internal sealed class ReplaceIntermediateVertexCommand : BaseIntermediateEndPointsCommand
    {
        public ReplaceIntermediateVertexCommand(BaseEndPoints endPoints) : base(endPoints)
        {

        }

        public override void Execute(IVertex vertex)
        {
            if (endPoints.markedToReplaceIntermediates.Count > 0)
            {
                var toReplace = endPoints.markedToReplaceIntermediates.Dequeue();
                int toReplaceIndex = endPoints.intermediates.IndexOf(toReplace);
                if (endPoints.intermediates.Remove(toReplace) && toReplaceIndex > -1)
                {
                    (toReplace as IVisualizable)?.VisualizeAsRegular();
                    endPoints.intermediates.Insert(toReplaceIndex, vertex);
                    (vertex as IVisualizable)?.VisualizeAsIntermediate();
                }
            }
        }

        public override bool IsTrue(IVertex vertex)
        {
            return endPoints.markedToReplaceIntermediates.Count > 0
                && !endPoints.IsEndPoint(vertex);
        }
    }
}
