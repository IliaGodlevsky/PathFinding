using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(0)]
    internal sealed class UnsetSourceCommand : BasePathfindingRangeCommand
    {
        public UnsetSourceCommand(BasePathfindingRange endPoints)
            : base(endPoints)
        {

        }

        public override void Execute(IVertex vertex)
        {
            vertex.AsVisualizable().VisualizeAsRegular();
            range.Source = NullVertex.Interface;
        }

        public override bool CanExecute(IVertex vertex)
        {
            return vertex.IsEqual(range.Source);
        }
    }
}