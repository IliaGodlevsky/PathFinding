using GraphLib.Base.EndPoints.EndPointsInspection;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;
using System.Linq;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    internal sealed class ReplaceIntermediateIsolatedVertexCondition
        : BaseIntermediateEndPointsInspection, IVertexCommand
    {
        public ReplaceIntermediateIsolatedVertexCondition(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public bool IsTrue(IVertex vertex)
        {
            return endPoints.HasSourceAndTargetSet()
                && !IsIntermediate(vertex)
                && HasIsolatedIntermediates
                && !endPoints.IsEndPoint(vertex);
        }

        public void Execute(IVertex vertex)
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
