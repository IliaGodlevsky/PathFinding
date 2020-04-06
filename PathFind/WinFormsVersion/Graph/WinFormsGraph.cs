using SearchAlgorythms.Top;
using System.Drawing;
using System.Windows.Forms;

namespace SearchAlgorythms.Graph
{
    public class WinFormsGraph : AbstractGraph
    {
        public event MouseEventHandler SetStart;
        public event MouseEventHandler SetEnd;

        public WinFormsGraph(IVertex[,] vertices) : base(vertices)
        {

        }

        public override Point GetIndexes(IVertex vertex)
        {
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)           
                    if (vertex.Location == vertices[i, j].Location)
                        return new Point(i, j);
            return new Point(Width, Height);
        }

        public override void ToDefault(IVertex vertex)
        {
            if (!vertex.IsObstacle)
                vertex.SetToDefault();
            (vertex as WinFormsVertex).MouseClick -= SetStart;
            (vertex as WinFormsVertex).MouseClick -= SetEnd;
            (vertex as WinFormsVertex).MouseClick += SetStart;
        }
    }
}
