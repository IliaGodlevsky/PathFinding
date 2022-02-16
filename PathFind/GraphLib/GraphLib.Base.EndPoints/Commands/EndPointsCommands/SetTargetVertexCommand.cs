using Common.Attrbiutes;
using GraphLib.Base.EndPoints.Attributes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [AttachedTo(typeof(SetEndPointsCommands)), Order(6)]
    internal sealed class SetTargetVertexCommand : BaseEndPointsCommand
    {
        public SetTargetVertexCommand(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(IVertex vertex)
        {
            endPoints.Target = vertex;
            (vertex as IVisualizable)?.VisualizeAsTarget();
        }

        public override bool IsTrue(IVertex vertex)
        {
            return !endPoints.Source.IsNull()
                && endPoints.Target.IsNull()
                && endPoints.CanBeEndPoint(vertex);
        }
    }
}
