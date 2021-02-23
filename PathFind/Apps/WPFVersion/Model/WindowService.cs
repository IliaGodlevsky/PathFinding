using Common.Extensions;
using GraphLib.Interface;
using GraphLib.Realizations;
using System.Windows;

namespace WPFVersion.Model
{
    internal static class WindowService
    {
        public static void Adjust(IGraph graph)
        {
            if (!graph.IsDefault())
            {
                int distanceBetweenVertices = Constants.DistanceBetweenVertices + Constants.VertexSize;
                var graph2d = graph as Graph2D;

                var mainWindowDesiredWidth = (graph2d.Width + 2) * distanceBetweenVertices;
                var mainWindowDesiredHeight = (graph2d.Length + 6) * distanceBetweenVertices;

                Application.Current.MainWindow.Width = mainWindowDesiredWidth;
                Application.Current.MainWindow.Height = mainWindowDesiredHeight;
            }
        }
    }
}
