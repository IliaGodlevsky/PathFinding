using System.Drawing;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Graph
{
    public class ConsoleGraph : AbstractGraph
    {
        public ConsoleGraph(IVertex[,] vertices) : base(vertices)
        {

        }

        public override Point GetIndexes(IVertex vertex)
        {
            return vertex.Location;
        }

        public override void ToDefault(IVertex vertex)
        {
            if (!vertex.IsObstacle)
                vertex.SetToDefault();
        }
    }
}
