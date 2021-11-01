using GraphLib.Base.EndPointsInspection.Abstractions;
using GraphLib.Interfaces;
using System.Linq;

namespace GraphLib.Base.VertexCondition.EndPointsConditions
{
    internal sealed class ReturnIntermediateColorCondition : BaseEndPointsInspection, IVertexCondition
    {
        public ReturnIntermediateColorCondition(BaseEndPoints endPoints) : base(endPoints)
        {
        }

        public void Execute(IVertex vertex)
        {
            (vertex as IVisualizable)?.VisualizeAsIntermediate();
        }

        public bool IsTrue(IVertex vertex)
        {
            return endPoints.IntermediateVertices.Contains(vertex);
        }
    }
}