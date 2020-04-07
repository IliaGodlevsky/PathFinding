using SearchAlgorythms.Top;

namespace SearchAlgorythms.Graph
{
    public class ConsoleGraph : AbstractGraph
    {
        public ConsoleGraph(IVertex[,] vertices) : base(vertices)
        {

        }

        public override void ToDefault(IVertex vertex)
        {
            if (!vertex.IsObstacle)
                vertex.SetToDefault();
        }
    }
}
