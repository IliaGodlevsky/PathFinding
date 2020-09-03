using GraphLibrary.Graph;
using GraphLibrary.Vertex;
using System.Windows.Forms;
using WinFormsVersion.Vertex;

namespace WinFormsVersion.Graph
{
    internal class WinFormsGraph : AbstractGraph
    {
        //public event MouseEventHandler SetStart;
        //public event MouseEventHandler SetEnd;

        public WinFormsGraph(IVertex[,] vertices) : base(vertices)
        {

        }

        //protected override void ToDefault(IVertex vertex)
        //{
        //    base.ToDefault(vertex);
        //    (vertex as WinFormsVertex).MouseClick -= SetStart;
        //    (vertex as WinFormsVertex).MouseClick -= SetEnd;
        //    (vertex as WinFormsVertex).MouseClick += SetStart;
        //}
    }
}
