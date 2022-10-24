using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(3)]
    internal sealed class ReplaceIntermediateIsolatedCommand : BaseIntermediatePathfindingRangeCommand
    {
        public ReplaceIntermediateIsolatedCommand(BasePathfindingRange endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(IVertex vertex)
        {
            var isolated = Intermediates.FirstOrNullVertex(v => v.IsIsolated());
            int isolatedIndex = Intermediates.IndexOf(isolated);
            Intermediates.Remove(isolated);
            MarkedToReplace.Remove(isolated);
            isolated.AsVisualizable().VisualizeAsRegular();
            Intermediates.Insert(isolatedIndex, vertex);
            vertex.AsVisualizable().VisualizeAsIntermediate();
        }

        public override bool CanExecute(IVertex vertex)
        {
            return range.HasSourceAndTargetSet()
                && HasIsolatedIntermediates
                && range.CanBeInPathfindingRange(vertex);
        }
    }
}