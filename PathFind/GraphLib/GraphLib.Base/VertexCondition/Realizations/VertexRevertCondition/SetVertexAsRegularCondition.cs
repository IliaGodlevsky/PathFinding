using GraphLib.Base.VertexCondition.Interface;
using GraphLib.Interfaces;

namespace GraphLib.Base.VertexCondition.Realizations.VertexRevertCondition
{
    internal sealed class SetVertexAsRegularCondition : IVertexCondition
    {
        public void Execute(IVertex vertex)
        {
            vertex.IsObstacle = false;
            (vertex as IVisualizable)?.VisualizeAsRegular();
        }

        public bool IsTrue(IVertex vertex)
        {
            return vertex.IsObstacle 
                && vertex is IVisualizable visualizable
                && !visualizable.IsVisualizedAsEndPoint;
        }
    }
}
