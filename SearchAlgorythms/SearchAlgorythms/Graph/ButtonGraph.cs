using SearchAlgorythms.Top;
using System.Drawing;
using System.Windows.Forms;

namespace SearchAlgorythms.Graph
{
    public class ButtonGraph : AbstractGraph
    {
        public event MouseEventHandler SetStart;
        public event MouseEventHandler SetEnd;

        public ButtonGraph(IGraphTop[,] tops) : base(tops)
        {

        }

        public override Point GetIndexes(IGraphTop top)
        {
            for (int i = 0; i < GetWidth(); i++)
                for (int j = 0; j < GetHeight(); j++)           
                    if (top.Location == buttons[i, j].Location)
                        return new Point(i, j);
            return new Point(GetWidth(), GetHeight());
        }

        public override void ToDefault(IGraphTop top)
        {
            if (!top.IsObstacle)
                top.SetToDefault();
            (top as GraphTop).MouseClick -= SetStart;
            (top as GraphTop).MouseClick -= SetEnd;
            (top as GraphTop).MouseClick += SetStart;
        }
    }
}
