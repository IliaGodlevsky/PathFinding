using GraphLib.Base.EndPoints.EndPointsInspection;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using NullObject.Extensions;

namespace GraphLib.Base.EndPoints.Commands.EndPointsCommands
{
    internal sealed class SetSourceVertexCondition
        : BaseEndPointsInspection, IVertexCommand
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
