using Common;
using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;
using System.Windows;

namespace WPFVersion.Model
{
    internal static class WindowService
    {
        public static void Adjust(IGraph graph)
        {
            if (!graph.IsDefault)
            {
                var graph2d = graph as Graph2D;

                var mainWindowDesiredWidth = (graph2d.Width + 2) * VertexParametres.SizeBetweenVertices;
                var mainWindowDesiredHeight = (graph2d.Length + 6) * VertexParametres.SizeBetweenVertices;

                Application.Current.MainWindow.Width = mainWindowDesiredWidth;
                Application.Current.MainWindow.Height = mainWindowDesiredHeight;
            }
        }
    }
}
