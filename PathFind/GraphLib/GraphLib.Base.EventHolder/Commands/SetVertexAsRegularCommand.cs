using GraphLib.Interfaces;

namespace GraphLib.Base.EventHolder.Commands
{
    internal sealed class SetVertexAsRegularCommand : IVertexCommand
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
