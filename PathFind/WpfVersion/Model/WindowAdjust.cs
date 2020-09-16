using System.Windows;
using GraphLibrary.Globals;
using GraphLibrary.Graphs.Interface;

namespace WpfVersion.Model
{
    internal static class WindowAdjust
    {
        public static void Adjust(IGraph graph)
        {
            if (graph == null)
                return;
            Application.Current.MainWindow.Width = (graph.Width + 1) * VertexSize.SIZE_BETWEEN_VERTICES + VertexSize.SIZE_BETWEEN_VERTICES;
            Application.Current.MainWindow.Height = (1 + graph.Height) * VertexSize.SIZE_BETWEEN_VERTICES +
                Application.Current.MainWindow.DesiredSize.Height;
        }
    }
}
