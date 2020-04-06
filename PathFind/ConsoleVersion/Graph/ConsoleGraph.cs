using System.Drawing;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Graph
{
    public class ConsoleGraph : AbstractGraph
    {
        public ConsoleGraph(IVertex[,] tops) : base(tops)
        {

        }

        public override Point GetIndexes(IVertex top)
        {
            return top.Location;
        }

        public override void ToDefault(IVertex top)
        {
            if (!top.IsObstacle)
                top.SetToDefault();
        }
    }
}
