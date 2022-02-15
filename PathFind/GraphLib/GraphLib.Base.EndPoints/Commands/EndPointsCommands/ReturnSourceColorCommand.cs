using Common.Attrbiutes;
using GraphLib.Base.EndPoints.Attributes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Attachment(typeof(ReturnColorsCommands)), Order(0)]
    internal sealed class ReturnSourceColorCommand : BaseEndPointsCommand
    {
        public ReturnSourceColorCommand(BaseEndPoints endPoints) : base(endPoints)
        {
        }

        public override void Execute(IVertex vertex)
        {
            (vertex as IVisualizable)?.VisualizeAsSource();
        }

        public override bool IsTrue(IVertex vertex)
        {
            return vertex.Equals(endPoints.Source);
        }
    }
}
