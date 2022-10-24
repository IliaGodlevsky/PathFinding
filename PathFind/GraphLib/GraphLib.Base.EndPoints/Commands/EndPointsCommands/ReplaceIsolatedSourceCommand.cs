using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(5)]
    internal sealed class ReplaceIsolatedSourceCommand : BasePathfindingRangeCommand
    {
        public ReplaceIsolatedSourceCommand(BasePathfindingRange endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(IVertex vertex)
        {
            Source.VisualizeAsRegular();
            range.Source = vertex;
            Source.VisualizeAsSource();
        }

        public override bool CanExecute(IVertex vertex)
        {
            return range.Source.IsIsolated()
                && !range.Source.IsNull()
                && range.CanBeInPathfindingRange(vertex);
        }
    }
}