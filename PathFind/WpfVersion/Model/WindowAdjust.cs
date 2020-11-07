using System.Windows;
using Common;
using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;

namespace WpfVersion.Model
{
    internal static class WindowAdjust
    {
        public static void Adjust(IGraph graph)
        {
            if (graph.IsDefault)
                return;
            Application.Current.MainWindow.Width = ((graph as Graph2d).Width + 1) * 
                VertexParametres.SizeBetweenVertices + VertexParametres.SizeBetweenVertices;
            Application.Current.MainWindow.Height = (1 + (graph as Graph2d).Length) * 
                VertexParametres.SizeBetweenVertices + Application.Current.MainWindow.DesiredSize.Height;
        }
    }
}
