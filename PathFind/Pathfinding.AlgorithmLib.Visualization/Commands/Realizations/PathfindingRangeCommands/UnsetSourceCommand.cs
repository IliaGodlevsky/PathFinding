using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(0)]
    internal sealed class UnsetSourceCommand<TVertex> : BaseEndPointsCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public UnsetSourceCommand(BaseEndPoints<TVertex> endPoints)
            : base(endPoints)
        {

        }

        public override void Execute(TVertex vertex)
        {
            vertex?.VisualizeAsRegular();
            endPoints.Source = default(TVertex);
        }

        public override bool CanExecute(TVertex vertex)
        {
            return endPoints.Source?.Equals(vertex) == true;
        }
    }
}