using Pathfinding.App.Console.Interface;

namespace Pathfinding.App.Console.Model.VertexActions
{
    internal sealed class ReverseVertexAction : IVertexAction
    {
        public void Invoke(Vertex vertex)
        {
            if (vertex.IsObstacle)
            {
                vertex.IsObstacle = false;
                vertex.VisualizeAsRegular();
            }
            else if (!vertex.IsVisualizedAsRange())
            {
                vertex.IsObstacle = true;
                vertex.VisualizeAsObstacle();
            }
        }
    }
}
