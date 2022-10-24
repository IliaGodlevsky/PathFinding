using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(7)]
    internal sealed class ReplaceIsolatedTargetCommand : BasePathfindingRangeCommand
    {
        public ReplaceIsolatedTargetCommand(BasePathfindingRange endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(IVertex vertex)
        {
            Target.VisualizeAsRegular();
            range.Target = vertex;
            Target.VisualizeAsTarget();
        }

        public override bool CanExecute(IVertex vertex)
        {
            return range.Target.IsIsolated()
                && !range.Target.IsNull()
                && range.CanBeInPathfindingRange(vertex);
        }
    }
}