using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(6)]
    internal sealed class SetTargetCommand : BasePathfindingRangeCommand
    {
        public SetTargetCommand(BasePathfindingRange endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(IVertex vertex)
        {
            range.Target = vertex;
            Target.VisualizeAsTarget();
        }

        public override bool CanExecute(IVertex vertex)
        {
            return !range.Source.IsNull()
                && range.Target.IsNull()
                && range.CanBeInPathfindingRange(vertex);
        }
    }
}
