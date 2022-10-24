using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(2)]
    internal sealed class RestoreMarkedToReplaceColorCommand : BaseIntermediatePathfindingRangeCommand
    {
        public RestoreMarkedToReplaceColorCommand(BasePathfindingRange endPoints)
            : base(endPoints)
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