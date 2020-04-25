using GraphLibrary.Graph;
using GraphLibrary.Vertex;

namespace ConsoleVersion.Graph
{
    public class ConsoleGraph : AbstractGraph
    {
        public ConsoleGraph(IVertex[,] vertices) : base(vertices)
        {

        }

        protected override void ToDefault(IVertex vertex)
        {
            if (!vertex.IsObstacle)
                vertex.SetToDefault();
        }
    }
}
