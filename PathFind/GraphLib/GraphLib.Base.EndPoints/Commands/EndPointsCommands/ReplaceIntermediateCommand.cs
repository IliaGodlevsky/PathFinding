using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Linq;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(8)]
    internal sealed class ReplaceIntermediateCommand : BaseIntermediatePathfindingRangeCommand
    {
        public ReplaceIntermediateCommand(BasePathfindingRange endPoints)
            : base(endPoints)
        {

        }

        public override void Execute(IVertex vertex)
        {
            var toRemove = MarkedToReplace.First();
            MarkedToReplace.Remove(toRemove);
            int toReplaceIndex = Intermediates.IndexOf(toRemove);
            Intermediates.Remove(toRemove);
            toRemove.AsVisualizable().VisualizeAsRegular();
            Intermediates.Insert(toReplaceIndex, vertex);
            vertex.AsVisualizable().VisualizeAsIntermediate();
        }

        public override bool CanExecute(IVertex vertex)
        {
            return MarkedToReplace.Count > 0
                && range.CanBeInPathfindingRange(vertex);
        }
    }
}
