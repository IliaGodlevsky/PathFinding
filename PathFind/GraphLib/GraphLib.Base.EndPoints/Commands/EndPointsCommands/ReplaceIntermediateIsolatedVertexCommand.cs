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
    [AttachedTo(typeof(SetEndPointsCommands)), Order(3)]
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
            var isolated = Intermediates.FirstOrDefault(v => v.IsIsolated());
            if (!isolated.IsNull())
            {
                int isolatedIndex = Intermediates.IndexOf(isolated);
                Intermediates.Remove(isolated);
                isolated.AsVisualizable().VisualizeAsRegular();
                Intermediates.Insert(isolatedIndex, vertex);
                vertex.AsVisualizable().VisualizeAsIntermediate();
            }
        }
    }
}
