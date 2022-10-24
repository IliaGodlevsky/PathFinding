using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(1)]
    internal sealed class UnsetTargetCommand : BasePathfindingRangeCommand
    {
        public UnsetTargetCommand(BasePathfindingRange endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(IVertex vertex)
        {
            vertex.AsVisualizable().VisualizeAsRegular();
            range.Target = NullVertex.Interface;
        }

        public override bool CanExecute(IVertex vertex)
        {
            return range.Target.IsEqual(vertex);
        }
    }
}