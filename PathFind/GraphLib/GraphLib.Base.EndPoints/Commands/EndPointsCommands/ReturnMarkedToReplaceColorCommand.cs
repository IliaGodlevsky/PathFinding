using Common.Attrbiutes;
using GraphLib.Base.EndPoints.Attributes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [AttachedTo(typeof(ReturnColorsCommands)), Order(2)]
    internal sealed class ReturnMarkedToReplaceColorCommand : BaseEndPointsCommand
    {
        public ReturnMarkedToReplaceColorCommand(BaseEndPoints endPoints) : base(endPoints)
        {
        }

        public override void Execute(IVertex vertex)
        {
            (vertex as IVisualizable)?.VisualizeAsIntermediate();
            (vertex as IVisualizable)?.VisualizeAsMarkedToReplaceIntermediate();
        }

        public override bool IsTrue(IVertex vertex)
        {
            return endPoints.markedToReplaceIntermediates.Contains(vertex);
        }
    }
}
