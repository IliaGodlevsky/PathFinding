using Common.Attrbiutes;
using GraphLib.Base.EndPoints.Attributes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Attachment(typeof(ReturnColorsCommands)), Order(3)]
    internal sealed class ReturnIntermediateColorCommand : BaseEndPointsCommand
    {
        public ReturnIntermediateColorCommand(BaseEndPoints endPoints) : base(endPoints)
        {
        }

        public override void Execute(IVertex vertex)
        {
            (vertex as IVisualizable)?.VisualizeAsIntermediate();
        }

        public override bool IsTrue(IVertex vertex)
        {
            return endPoints.intermediates.Contains(vertex);
        }
    }
}