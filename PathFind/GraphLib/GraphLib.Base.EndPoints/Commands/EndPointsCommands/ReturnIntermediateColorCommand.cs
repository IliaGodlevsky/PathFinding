using Common.Attrbiutes;
using GraphLib.Base.EndPoints.Attributes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [AttachedTo(typeof(ReturnColorsCommands)), Order(3)]
    internal sealed class ReturnIntermediateColorCommand : BaseIntermediateEndPointsCommand
    {
        public ReturnIntermediateColorCommand(BaseEndPoints endPoints) : base(endPoints)
        {
        }

        public override void Execute(IVertex vertex)
        {
            vertex.AsVisualizable().VisualizeAsIntermediate();
        }

        public override bool IsTrue(IVertex vertex)
        {
            return IsIntermediate(vertex);
        }
    }
}