using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(2)]
    internal sealed class UnsetIntermediateCommand : BaseIntermediatePathfindingRangeCommand
    {
        public UnsetIntermediateCommand(BasePathfindingRange endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(IVertex vertex)
        {
            if (IsMarkedToReplace(vertex))
            {
                MarkedToReplace.Remove(vertex);
            }
            Intermediates.Remove(vertex);
            vertex.AsVisualizable().VisualizeAsRegular();
        }

        public override bool CanExecute(IVertex vertex)
        {
            return IsIntermediate(vertex);
        }
    }
}