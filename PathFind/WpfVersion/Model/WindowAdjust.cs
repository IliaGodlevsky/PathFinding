using System.Windows;
using GraphLibrary.Globals;
using GraphLibrary.Graphs;
using GraphLibrary.Graphs.Interface;

namespace WpfVersion.Model
{
    internal static class WindowAdjust
    {
        public static void Adjust(IGraph graph)
        {
            if (graph == null)
                return;
            Application.Current.MainWindow.Width = ((graph as Graph).Width + 1) * 
                VertexParametres.SizeBetweenVertices + VertexParametres.SizeBetweenVertices;
            Application.Current.MainWindow.Height = (1 + (graph as Graph).Length) * 
                VertexParametres.SizeBetweenVertices +
                Application.Current.MainWindow.DesiredSize.Height;
        }
    }
}
