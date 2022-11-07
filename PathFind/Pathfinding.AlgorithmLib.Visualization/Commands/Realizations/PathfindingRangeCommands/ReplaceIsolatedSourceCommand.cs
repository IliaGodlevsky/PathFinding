using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(5)]
    internal sealed class ReplaceIsolatedSourceCommand<TVertex> : BaseEndPointsCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public ReplaceIsolatedSourceCommand(BaseEndPoints<TVertex> endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(TVertex vertex)
        {
            Source.VisualizeAsRegular();
            endPoints.Source = vertex;
            Source.VisualizeAsSource();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return endPoints.Source.IsIsolated()
                && !endPoints.Source.IsNull()
                && endPoints.CanBeEndPoint(vertex);
        }
    }
}