using Common.Attrbiutes;
using GraphLib.Base.EndPoints.Attributes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [AttachedTo(typeof(SetEndPointsCommands)), Order(1)]
    internal sealed class UnsetTargetVertexCommand : BaseEndPointsCommand
    {
        public UnsetTargetVertexCommand(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public override bool IsTrue(IVertex vertex)
        {
            return endPoints.Target.IsEqual(vertex);
        }

        public override void Execute(IVertex vertex)
        {
            vertex.AsVisualizable().VisualizeAsRegular();
            endPoints.Target = NullVertex.Instance;
        }
    }
}
