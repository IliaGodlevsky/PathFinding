using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(4)]
    internal sealed class SetSourceCommand<TVertex> : BaseEndPointsCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public SetSourceCommand(BaseEndPoints<TVertex> endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(TVertex vertex)
        {
            endPoints.Source = vertex;
            Source.VisualizeAsSource();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return endPoints.Source == null
                && endPoints.CanBeEndPoint(vertex);
        }
    }
}