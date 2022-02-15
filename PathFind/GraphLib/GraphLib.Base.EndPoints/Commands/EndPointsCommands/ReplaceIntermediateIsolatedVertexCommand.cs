using Common.Attrbiutes;
using GraphLib.Base.EndPoints.Attributes;
using GraphLib.Base.EndPoints.BaseCommands;
using GraphLib.Base.EndPoints.Commands.VerticesCommands;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Linq;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    [Attachment(typeof(SetEndPointsCommands)), Order(3)]
    internal sealed class ReplaceIntermediateIsolatedVertexCommand : BaseIntermediateEndPointsCommand
    {
        public ReplaceIntermediateIsolatedVertexCommand(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public override bool IsTrue(IVertex vertex)
        {
            return endPoints.HasSourceAndTargetSet()
                && !IsIntermediate(vertex)
                && HasIsolatedIntermediates
                && !endPoints.IsEndPoint(vertex);
        }

        public override void Execute(IVertex vertex)
        {
            var isolated = endPoints.intermediates.FirstOrDefault(v => v.IsIsolated());
            if (!isolated.IsNull())
            {
                int isolatedIndex = endPoints.intermediates.IndexOf(isolated);
                endPoints.intermediates.Remove(isolated);
                (isolated as IVisualizable)?.VisualizeAsRegular();
                endPoints.intermediates.Insert(isolatedIndex, vertex);
                (vertex as IVisualizable)?.VisualizeAsIntermediate();
            }
        }
    }
}
