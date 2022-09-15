using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(0)]
    internal sealed class UnsetSourceCommand : BaseEndPointsCommand
    {
        public UnsetSourceCommand(BaseEndPoints endPoints)
            : base(endPoints)
        {

        }

        public override void Execute(IVertex vertex)
        {
            vertex.AsVisualizable().VisualizeAsRegular();
            endPoints.Source = NullVertex.Interface;
        }

        public override bool CanExecute(IVertex vertex)
        {
            return vertex.IsEqual(endPoints.Source);
        }
    }
}