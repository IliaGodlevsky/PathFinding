using Common.Attrbiutes;
using GraphLib.Base.EndPoints.Attributes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [AttachedTo(typeof(IntermediateToReplaceCommands)), Order(0)]
    internal sealed class CancelMarkToReplaceCommand : BaseIntermediateEndPointsCommand
    {
        public CancelMarkToReplaceCommand(BaseEndPoints endPoints) : base(endPoints)
        {

        }

        public override void Execute(IVertex vertex)
        {
            MarkedToReplace.Remove(vertex);
            vertex.AsVisualizable().VisualizeAsIntermediate();
        }

        public override bool IsTrue(IVertex vertex)
        {
            return IsMarkedToReplace(vertex);
        }
    }
}
