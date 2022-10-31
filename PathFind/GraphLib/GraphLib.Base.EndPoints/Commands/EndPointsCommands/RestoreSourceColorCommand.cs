using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(0)]
    internal sealed class RestoreSourceColorCommand<TVertex> : BaseEndPointsCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public RestoreSourceColorCommand(BaseEndPoints<TVertex> endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(TVertex vertex)
        {
            vertex.VisualizeAsSource();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return vertex.Equals(endPoints.Source);
        }
    }
}