using Common.Attrbiutes;
using Common.Extensions;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(1)]
    internal sealed class MarkToReplaceCommand<TVertex> : BaseIntermediateEndPointsCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public MarkToReplaceCommand(BaseEndPoints<TVertex> endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(TVertex vertex)
        {
            MarkedToReplace.Add(vertex);
            vertex.VisualizeAsMarkedToReplaceIntermediate();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return !vertex.IsOneOf(endPoints.Source, endPoints.Target)
                && IsIntermediate(vertex)
                && !IsMarkedToReplace(vertex);
        }
    }
}
