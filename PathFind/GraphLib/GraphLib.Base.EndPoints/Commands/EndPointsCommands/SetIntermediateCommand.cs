using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(9)]
    internal sealed class SetIntermediateCommand<TVertex> : BaseIntermediateEndPointsCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public SetIntermediateCommand(BaseEndPoints<TVertex> endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(TVertex vertex)
        {
            Intermediates.Add(vertex);
            vertex.VisualizeAsIntermediate();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return endPoints.HasSourceAndTargetSet()
                && endPoints.CanBeEndPoint(vertex);
        }
    }
}