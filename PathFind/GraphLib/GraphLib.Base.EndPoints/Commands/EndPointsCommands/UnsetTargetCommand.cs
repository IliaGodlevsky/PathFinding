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
    internal sealed class UnsetTargetCommand : BaseEndPointsCommand
    {
        public UnsetTargetCommand(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public override bool CanExecute(IVertex vertex)
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
