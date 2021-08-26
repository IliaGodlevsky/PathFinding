using GraphLib.Interfaces;
using NullObject.Extensions;

namespace GraphLib.Base.BaseEndPointsConditions
{
    internal sealed class SetSourceVertexCondition
        : BaseEndPointsCondition, IEndPointsCondition
    {
        public SetSourceVertexCondition(BaseEndPoints endPoints)
            : base(endPoints)
        {
        }

        public bool IsTrue(IVertex vertex)
        {
            return endPoints.Source.IsNull()
                && endPoints.CanBeEndPoint(vertex);
        }

        public void Execute(IVertex vertex)
        {
            endPoints.Source = vertex;
            (vertex as IVisualizable)?.VisualizeAsSource();
        }
    }
}
