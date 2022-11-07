using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(6)]
    internal sealed class SetTargetCommand<TVertex> : BaseEndPointsCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public SetTargetCommand(BaseEndPoints<TVertex> endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(TVertex vertex)
        {
            endPoints.Target = vertex;
            Target.VisualizeAsTarget();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return !endPoints.Source.IsNull()
                && endPoints.Target.IsNull()
                && endPoints.CanBeEndPoint(vertex);
        }
    }
}
