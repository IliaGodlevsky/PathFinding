using GraphLib.Interfaces;

namespace GraphLib.Base.EventHolder.Commands
{
    internal sealed class SetVertexAsRegularCommand<TVertex> : IVertexCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public void Execute(TVertex vertex)
        {
            vertex.IsObstacle = false;
            vertex.VisualizeAsRegular();
        }

        public bool CanExecute(TVertex vertex)
        {
            return vertex.IsObstacle && !vertex.IsVisualizedAsEndPoint;
        }
    }
}
