using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(7)]
    internal sealed class ReplaceIsolatedTargetCommand<TVertex> : BaseEndPointsCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public ReplaceIsolatedTargetCommand(BaseEndPoints<TVertex> endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(TVertex vertex)
        {
            Target.VisualizeAsRegular();
            endPoints.Target = vertex;
            Target.VisualizeAsTarget();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return endPoints.Target.IsIsolated()
                && !endPoints.Target.IsNull()
                && endPoints.CanBeEndPoint(vertex);
        }
    }
}