using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(1)]
    internal sealed class UnsetTargetCommand<TVertex> : BaseEndPointsCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public UnsetTargetCommand(BaseEndPoints<TVertex> endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(TVertex vertex)
        {
            vertex?.VisualizeAsRegular();
            endPoints.Target = default(TVertex);
        }

        public override bool CanExecute(TVertex vertex)
        {
            return endPoints.Target?.Equals(vertex) == true;
        }
    }
}