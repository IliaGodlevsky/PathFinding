using Common.Attrbiutes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [AttachedTo(typeof(IntermediateToReplaceCommands)), Order(1)]
    internal sealed class MarkToReplaceCommand : BaseIntermediateEndPointsCommand
    {
        public MarkToReplaceCommand(BaseEndPoints endPoints) : base(endPoints)
        {
        }

        public override void Execute(IVertex vertex)
        {
            MarkedToReplace.Add(vertex);
            vertex.AsVisualizable().VisualizeAsMarkedToReplaceIntermediate();
        }

        public override bool CanExecute(IVertex vertex)
        {
            return !vertex.IsOneOf(endPoints.Source, endPoints.Target)
                && IsIntermediate(vertex)
                && !IsMarkedToReplace(vertex);
        }
    }
}
