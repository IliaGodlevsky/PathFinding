using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(0)]
    internal sealed class CancelMarkToReplaceCommand<TVertex> : BaseIntermediateEndPointsCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public CancelMarkToReplaceCommand(BaseEndPoints<TVertex> endPoints)
            : base(endPoints)
        {

        }

        public override void Execute(TVertex vertex)
        {
            MarkedToReplace.Remove(vertex);
            vertex.VisualizeAsIntermediate();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return IsMarkedToReplace(vertex);
        }
    }
}
