using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Linq;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(3)]
    internal sealed class ReplaceIntermediateIsolatedCommand<TVertex> : BaseIntermediateEndPointsCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public ReplaceIntermediateIsolatedCommand(BaseEndPoints<TVertex> endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(TVertex vertex)
        {
            var isolated = Intermediates.First(v => v.IsIsolated());
            int isolatedIndex = Intermediates.IndexOf(isolated);
            Intermediates.Remove(isolated);
            MarkedToReplace.Remove(isolated);
            isolated.VisualizeAsRegular();
            Intermediates.Insert(isolatedIndex, vertex);
            vertex.VisualizeAsIntermediate();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return endPoints.HasSourceAndTargetSet()
                && HasIsolatedIntermediates
                && endPoints.CanBeEndPoint(vertex);
        }
    }
}