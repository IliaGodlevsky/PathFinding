using Common.Attrbiutes;
using GraphLib.Base.EndPoints;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;

namespace GraphLib.Base.VertexCondition.EndPointsConditions
{
    [AttachedTo(typeof(SetEndPointsCommands)), Order(0)]
    internal sealed class UnsetSourceCommand : BaseEndPointsCommand
    {
        public UnsetSourceCommand(BaseEndPoints endPoints)
            : base(endPoints)
        {

        }

        public override bool CanExecute(IVertex vertex)
        {
            return vertex.IsEqual(endPoints.Source);
        }

        public override void Execute(IVertex vertex)
        {
            vertex.AsVisualizable().VisualizeAsRegular();
            endPoints.Source = NullVertex.Instance;
        }
    }
}
