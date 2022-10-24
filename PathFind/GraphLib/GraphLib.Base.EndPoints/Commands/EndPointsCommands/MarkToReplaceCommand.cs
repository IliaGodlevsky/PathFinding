using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(1)]
    internal sealed class MarkToReplaceCommand : BaseIntermediatePathfindingRangeCommand
    {
        public MarkToReplaceCommand(BasePathfindingRange endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(IVertex vertex)
        {
            MarkedToReplace.Add(vertex);
            vertex.AsVisualizable().VisualizeAsMarkedToReplaceIntermediate();
        }

        public override bool CanExecute(IVertex vertex)
        {
            return !vertex.IsOneOf(range.Source, range.Target)
                && IsIntermediate(vertex)
                && !IsMarkedToReplace(vertex);
        }
    }
}
