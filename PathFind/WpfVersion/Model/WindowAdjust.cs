using System.Linq;
using System.Windows;
using Common;
using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;
using WpfVersion.Model.Vertex;

namespace WpfVersion.Model
{
    internal static class WindowAdjust
    {
        public static void Adjust(IGraph graph)
        {
            if (!graph.IsDefault)
            {
                var graph2d = graph as Graph2D;

                var mainWindowDesiredWidth = (graph2d.Width + 2) * VertexParametres.SizeBetweenVertices;
                var mainWindowDesiredHeight = (graph2d.Length + 2) * VertexParametres.SizeBetweenVertices;

                Application.Current.MainWindow.Width = mainWindowDesiredWidth;
                Application.Current.MainWindow.Height = mainWindowDesiredHeight;
            }
        }
    }
}
