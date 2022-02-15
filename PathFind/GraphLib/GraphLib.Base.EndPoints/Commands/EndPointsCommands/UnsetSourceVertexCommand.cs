using Common.Attrbiutes;
using GraphLib.Base.EndPoints;
using GraphLib.Base.EndPoints.Attributes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;

namespace GraphLib.Base.VertexCondition.EndPointsConditions
{
    [Attachment(typeof(SetEndPointsCommands)), Order(0)]
    internal sealed class UnsetSourceVertexCommand : BaseEndPointsCommand
    {
        public UnsetSourceVertexCommand(BaseEndPoints endPoints)
            : base(endPoints)
        {

        }

        public override bool IsTrue(IVertex vertex)
        {
            return vertex.IsEqual(endPoints.Source);
        }

        public override void Execute(IVertex vertex)
        {
            (vertex as IVisualizable)?.VisualizeAsRegular();
            endPoints.Source = NullVertex.Instance;
        }
    }
}
