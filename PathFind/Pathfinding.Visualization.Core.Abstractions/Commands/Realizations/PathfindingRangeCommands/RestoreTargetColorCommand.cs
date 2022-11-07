using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(1)]
    internal sealed class RestoreTargetColorCommand<TVertex> : BaseEndPointsCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public RestoreTargetColorCommand(BaseEndPoints<TVertex> endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(TVertex vertex)
        {
            vertex.VisualizeAsTarget();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return vertex.Equals(endPoints.Target);
        }
    }
}