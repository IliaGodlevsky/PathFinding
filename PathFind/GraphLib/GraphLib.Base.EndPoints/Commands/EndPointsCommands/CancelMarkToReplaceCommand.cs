using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(0)]
    internal sealed class CancelMarkToReplaceCommand : BaseIntermediateEndPointsCommand
    {
        public CancelMarkToReplaceCommand(BaseEndPoints endPoints)
            : base(endPoints)
        {

        }

        public override void Execute(IVertex vertex)
        {
            MarkedToReplace.Remove(vertex);
            vertex.AsVisualizable().VisualizeAsIntermediate();
        }

        public override bool CanExecute(IVertex vertex)
        {
            return IsMarkedToReplace(vertex);
        }
    }
}
