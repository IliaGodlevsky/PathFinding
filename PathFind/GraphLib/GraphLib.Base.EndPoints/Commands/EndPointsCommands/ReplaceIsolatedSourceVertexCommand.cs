using Common.Attrbiutes;
using GraphLib.Base.EndPoints.Attributes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [AttachedTo(typeof(SetEndPointsCommands)), Order(5)]
    internal sealed class ReplaceIsolatedSourceVertexCommand : BaseEndPointsCommand
    {
        public ReplaceIsolatedSourceVertexCommand(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(IVertex vertex)
        {
            Source.VisualizeAsRegular();
            endPoints.Source = vertex;
            Source.VisualizeAsSource();
        }

        public override bool IsTrue(IVertex vertex)
        {
            return endPoints.Source.IsIsolated()
                && !endPoints.Source.IsNull()
                && !endPoints.IsEndPoint(vertex)
                && endPoints.CanBeEndPoint(vertex);
        }
    }
}
