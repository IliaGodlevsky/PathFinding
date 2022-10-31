using GraphLib.Interfaces;

namespace GraphLib.Base.EventHolder.Commands
{
    internal sealed class SetVertexAsObstacleCommand<TVertex> : IVertexCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public void Execute(TVertex vertex)
        {
            vertex.IsObstacle = true;
            vertex.VisualizeAsObstacle();
        }

        public bool CanExecute(TVertex vertex)
        {
            return !vertex.IsObstacle && !vertex.IsVisualizedAsEndPoint;
        }
    }
}
