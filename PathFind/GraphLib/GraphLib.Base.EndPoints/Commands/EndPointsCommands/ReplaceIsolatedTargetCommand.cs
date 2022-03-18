using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [AttachedTo(typeof(SetEndPointsCommands)), Order(7)]
    internal sealed class ReplaceIsolatedTargetCommand : BaseEndPointsCommand
    {
        public ReplaceIsolatedTargetCommand(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(IVertex vertex)
        {
            Target.VisualizeAsRegular();
            endPoints.Target = vertex;
            Target.VisualizeAsTarget();
        }

        public override bool CanExecute(IVertex vertex)
        {
            return endPoints.Target.IsIsolated()
                && !endPoints.Target.IsNull()
                && endPoints.CanBeEndPoint(vertex);
        }
    }
}