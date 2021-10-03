using GraphLib.Base.EndPointsInspection.Abstractions;
using GraphLib.Base.VertexCondition.Interface;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.VertexCondition.Realizations.EndPointsConditions
{
    internal sealed class SetIntermediateVertexCondition
        : BaseEndPointsInspection, IVertexCondition
    {
        public SetIntermediateVertexCondition(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public void Execute(IVertex vertex)
        {
            endPoints.intermediates.Add(vertex);
            (vertex as IVisualizable)?.VisualizeAsIntermediate();
        }

        public bool IsTrue(IVertex vertex)
        {
            return endPoints.HasSourceAndTargetSet();
        }
    }
}
