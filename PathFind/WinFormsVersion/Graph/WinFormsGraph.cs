using GraphLibrary.Graph;
using GraphLibrary.Vertex;
using System.Windows.Forms;
using WinFormsVersion.Vertex;

namespace WinFormsVersion.Graph
{
    public class WinFormsGraph : AbstractGraph
    {
        public event MouseEventHandler SetStart;
        public event MouseEventHandler SetEnd;

        public WinFormsGraph(IVertex[,] vertices) : base(vertices)
        {
            
        }

        protected override void ToDefault(IVertex vertex)
        {
            if (!vertex.IsObstacle)
                vertex.SetToDefault();
            (vertex as WinFormsVertex).MouseClick -= SetStart;
            (vertex as WinFormsVertex).MouseClick -= SetEnd;
            (vertex as WinFormsVertex).MouseClick += SetStart;
        }
    }
}
