using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(5)]
    internal sealed class ReplaceIsolatedSourceCommand : BaseEndPointsCommand
    {
        public ReplaceIsolatedSourceCommand(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(IVertex vertex)
        {
            Source.VisualizeAsRegular();
            endPoints.Source = vertex;
            Source.VisualizeAsSource();
        }

        public override bool CanExecute(IVertex vertex)
        {
            return endPoints.Source.IsIsolated()
                && !endPoints.Source.IsNull()
                && endPoints.CanBeEndPoint(vertex);
        }
    }
}