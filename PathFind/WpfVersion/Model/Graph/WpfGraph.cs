using GraphLibrary.Graph;
using GraphLibrary.Vertex;
using System.Windows;
using WpfVersion.Model.Vertex;

namespace WpfVersion.Model.Graph
{

    public class WpfGraph : AbstractGraph
    {
        public event RoutedEventHandler SetStart;
        public event RoutedEventHandler SetEnd;
        public WpfGraph(IVertex[,] vertices) : base(vertices)
        {

        }

        protected override void ToDefault(IVertex vertex)
        {
            if (!vertex.IsObstacle)
                vertex.SetToDefault();
            (vertex as WpfVertex).Click -= SetStart;
            (vertex as WpfVertex).Click -= SetEnd;
            (vertex as WpfVertex).Click += SetStart;
        }
    }
}
