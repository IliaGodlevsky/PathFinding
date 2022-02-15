using GraphLib.Interfaces;

namespace GraphLib.Base.EventHolder.Commands
{
    internal sealed class SetVertexAsObstacleCommand : IVertexCommand
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
