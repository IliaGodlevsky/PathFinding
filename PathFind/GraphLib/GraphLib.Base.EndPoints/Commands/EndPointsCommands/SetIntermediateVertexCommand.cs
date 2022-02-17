using Common.Attrbiutes;
using GraphLib.Base.EndPoints.Attributes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [AttachedTo(typeof(SetEndPointsCommands)), Order(9)]
    internal sealed class SetIntermediateVertexCommand : BaseIntermediateEndPointsCommand
    {
        public SetIntermediateVertexCommand(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(IVertex vertex)
        {
            Intermediates.Add(vertex);
            LastIntermediate.VisualizeAsIntermediate();
        }

        public override bool IsTrue(IVertex vertex)
        {
            return endPoints.HasSourceAndTargetSet();
        }
    }
}
