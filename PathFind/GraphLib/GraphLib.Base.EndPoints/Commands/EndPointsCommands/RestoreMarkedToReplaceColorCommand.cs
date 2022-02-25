using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [AttachedTo(typeof(RestoreColorsCommands)), Order(2)]
    internal sealed class RestoreMarkedToReplaceColorCommand : BaseIntermediateEndPointsCommand
    {
        public RestoreMarkedToReplaceColorCommand(BaseEndPoints endPoints) : base(endPoints)
        {
        }

        public override void Execute(IVertex vertex)
        {
            vertex.AsVisualizable().VisualizeAsIntermediate();
            vertex.AsVisualizable().VisualizeAsMarkedToReplaceIntermediate();
        }

        public override bool CanExecute(IVertex vertex)
        {
            return IsMarkedToReplace(vertex);
        }
    }
}
