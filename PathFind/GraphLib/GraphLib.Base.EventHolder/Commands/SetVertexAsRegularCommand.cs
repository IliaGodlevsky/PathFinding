using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.Base.EventHolder.Commands
{
    internal sealed class SetVertexAsRegularCommand : IVertexCommand
    {
        public void Execute(IVertex vertex)
        {
            vertex.IsObstacle = false;
            vertex.AsVisualizable().VisualizeAsRegular();
        }

        public bool CanExecute(IVertex vertex)
        {
            return vertex.IsObstacle && !vertex.AsVisualizable().IsVisualizedAsEndPoint;
        }
    }
}
