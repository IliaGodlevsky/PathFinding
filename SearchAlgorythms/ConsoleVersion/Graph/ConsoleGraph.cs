using System.Drawing;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Graph
{
    public class ConsoleGraph : AbstractGraph
    {
        public ConsoleGraph(IGraphTop[,] tops) : base(tops)
        {

        }

        public override Point GetIndexes(IGraphTop top)
        {
            return new Point(top.Location.X, top.Location.Y);
        }

        public override void ToDefault(IGraphTop top)
        {
            if (!top.IsObstacle)
                top.SetToDefault();
        }
    }
}
