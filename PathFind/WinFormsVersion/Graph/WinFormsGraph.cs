using SearchAlgorythms.Top;
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
