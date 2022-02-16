using Common.Attrbiutes;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Base.EndPoints.Attributes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.ReplaceIntermediatesConditions
{
    [AttachedTo(typeof(IntermediateToReplaceCommands)), Order(0)]
    internal sealed class CancelMarkToReplaceCommand : BaseIntermediateEndPointsCommand
    {
        public CancelMarkToReplaceCommand(BaseEndPoints endPoints) : base(endPoints)
        {

        }

        public override void Execute(IVertex vertex)
        {
            endPoints.markedToReplaceIntermediates.Remove(vertex);
            (vertex as IVisualizable)?.VisualizeAsIntermediate();
        }

        public override bool IsTrue(IVertex vertex)
        {
            return IsMarkedToReplace(vertex);
        }
    }
}
