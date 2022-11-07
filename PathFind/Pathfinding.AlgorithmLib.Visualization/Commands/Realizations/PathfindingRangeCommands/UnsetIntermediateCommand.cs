using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(2)]
    internal sealed class UnsetIntermediateCommand<TVertex> : BaseIntermediateEndPointsCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public UnsetIntermediateCommand(BaseEndPoints<TVertex> endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(TVertex vertex)
        {
            if (IsMarkedToReplace(vertex))
            {
                MarkedToReplace.Remove(vertex);
            }
            Intermediates.Remove(vertex);
            vertex.VisualizeAsRegular();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return IsIntermediate(vertex);
        }
    }
}