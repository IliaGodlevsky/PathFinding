using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Linq;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(8)]
    internal sealed class ReplaceIntermediateCommand<TVertex> : BaseIntermediateEndPointsCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public ReplaceIntermediateCommand(BaseEndPoints<TVertex> endPoints)
            : base(endPoints)
        {

        }

        public override void Execute(TVertex vertex)
        {
            var toRemove = MarkedToReplace.First();
            MarkedToReplace.Remove(toRemove);
            int toReplaceIndex = Intermediates.IndexOf(toRemove);
            Intermediates.Remove(toRemove);
            toRemove.VisualizeAsRegular();
            Intermediates.Insert(toReplaceIndex, vertex);
            vertex.VisualizeAsIntermediate();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return MarkedToReplace.Count > 0
                && endPoints.CanBeEndPoint(vertex);
        }
    }
}
