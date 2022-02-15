using Common.Attrbiutes;
using GraphLib.Base.EndPoints.Attributes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Attachment(typeof(ReturnColorsCommands)), Order(1)]
    internal sealed class ReturnTargetColorCommand : BaseEndPointsCommand
    {
        public ReturnTargetColorCommand(BaseEndPoints endPoints) : base(endPoints)
        {
        }

        public override void Execute(IVertex vertex)
        {
            (vertex as IVisualizable)?.VisualizeAsTarget();
        }

        public override bool IsTrue(IVertex vertex)
        {
            return vertex.Equals(endPoints.Target);
        }
    }
}