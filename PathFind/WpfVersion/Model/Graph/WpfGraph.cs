using GraphLibrary.Graph;
using GraphLibrary.Vertex;
using System.Windows.Input;
using WpfVersion.Model.Vertex;

namespace WpfVersion.Model.Graph
{
    internal class WpfGraph : AbstractGraph
    {
        public event MouseButtonEventHandler SetStart;
        public event MouseButtonEventHandler SetEnd;
        public WpfGraph(IVertex[,] vertices) : base(vertices)
        {

        }

        protected override void ToDefault(IVertex vertex)
        {
            base.ToDefault(vertex);
            (vertex as WpfVertex).MouseLeftButtonDown -= SetStart;
            (vertex as WpfVertex).MouseLeftButtonDown -= SetEnd;
            (vertex as WpfVertex).MouseLeftButtonDown += SetStart;
        }
    }
}
