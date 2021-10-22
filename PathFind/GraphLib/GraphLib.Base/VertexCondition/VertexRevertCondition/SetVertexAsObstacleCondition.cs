using GraphLib.Interfaces;

namespace GraphLib.Base.VertexCondition.VertexRevertCondition
{
    internal sealed class SetVertexAsObstacleCondition : IVertexCondition
    {
        public void Execute(IVertex vertex)
        {
            vertex.IsObstacle = true;
            (vertex as IVisualizable)?.VisualizeAsObstacle();
        }

        public bool IsTrue(IVertex vertex)
        {
            return !vertex.IsObstacle
                && vertex is IVisualizable visualizable
                && !visualizable.IsVisualizedAsEndPoint;
        }
    }
}
