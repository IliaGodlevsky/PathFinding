using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Order(2)]
    internal sealed class RestoreMarkedToReplaceColorCommand<TVertex> : BaseIntermediateEndPointsCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public RestoreMarkedToReplaceColorCommand(BaseEndPoints<TVertex> endPoints)
            : base(endPoints)
        {
        }

        public override void Execute(TVertex vertex)
        {
            vertex.VisualizeAsIntermediate();
            vertex.VisualizeAsMarkedToReplaceIntermediate();
        }

        public override bool CanExecute(TVertex vertex)
        {
            return IsMarkedToReplace(vertex);
        }
    }
}