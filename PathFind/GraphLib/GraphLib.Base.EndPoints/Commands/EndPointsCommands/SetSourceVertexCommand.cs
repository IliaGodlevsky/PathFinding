using Common.Attrbiutes;
using GraphLib.Base.EndPoints.Attributes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Attachment(typeof(SetEndPointsCommands)), Order(4)]
    internal sealed class SetSourceVertexCommand : BaseEndPointsCommand
    {
        public SetSourceVertexCommand(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public override bool IsTrue(IVertex vertex)
        {
            return endPoints.Source.IsNull()
                && endPoints.CanBeEndPoint(vertex);
        }

        public override void Execute(IVertex vertex)
        {
            endPoints.Source = vertex;
            (vertex as IVisualizable)?.VisualizeAsSource();
        }
    }
}
