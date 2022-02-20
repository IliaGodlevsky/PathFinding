using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EventHolder.Commands
{
    internal sealed class SetVertexAsObstacleCommand : IVertexCommand
    {
        public void Execute(IVertex vertex)
        {
            vertex.IsObstacle = true;
            vertex.AsVisualizable().VisualizeAsObstacle();
        }

        public bool CanExecute(IVertex vertex)
        {
            return !vertex.IsObstacle && !vertex.AsVisualizable().IsVisualizedAsEndPoint;
        }
    }
}
