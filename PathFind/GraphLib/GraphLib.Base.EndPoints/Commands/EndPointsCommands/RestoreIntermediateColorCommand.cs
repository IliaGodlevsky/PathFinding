using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(3)]
    internal sealed class RestoreIntermediateColorCommand : BaseIntermediateEndPointsCommand
    {
        public RestoreIntermediateColorCommand(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(IVertex vertex)
        {
            vertex.AsVisualizable().VisualizeAsIntermediate();
        }

        public override bool CanExecute(IVertex vertex)
        {
            return IsIntermediate(vertex);
        }
    }
}