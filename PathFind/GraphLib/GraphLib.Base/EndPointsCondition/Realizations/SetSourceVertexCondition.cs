using GraphLib.Base.EndPointsCondition.Interface;
using GraphLib.Base.EndPointsInspection.Abstractions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;

namespace GraphLib.Base.EndPointsCondition.Realizations
{
    internal sealed class SetSourceVertexCondition
        : BaseEndPointsInspection, IEndPointsCondition
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
