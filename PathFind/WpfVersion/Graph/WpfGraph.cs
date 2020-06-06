using GraphLibrary.Graph;
using GraphLibrary.Vertex;
using System.Windows.Input;
using WpfVersion.Vertex;

namespace WpfVersion.Graph
{
    public class WpfGraph : AbstractGraph
    {
        public event MouseButtonEventHandler SetStart;
        public event MouseButtonEventHandler SetEnd;

        public WpfGraph(IVertex[,] vertices) : base(vertices)
        {

        }

        protected override void ToDefault(IVertex vertex)
        {
            if (!vertex.IsObstacle)
                vertex.SetToDefault();
            (vertex as WpfVertex).MouseLeftButtonDown -= SetStart;
            (vertex as WpfVertex).MouseLeftButtonDown -= SetEnd;
            (vertex as WpfVertex).MouseLeftButtonDown += SetStart;
        }
    }
}
