using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using System.Windows;

namespace WPFVersion.Model
{
    internal static class WindowService
    {
        private const int WidthOffset = 2;
        private const int LengthOffset = 6;

        private const int DistanceBetweenVertices
            = Constants.DistanceBetweenVertices + Constants.VertexSize;

        public static void Adjust(IGraph graph)
        {
            if (graph is Graph2D graph2D)
            {
                var window = Application.Current?.MainWindow;
                if (window != null)
                {
                    window.Width = CountDesiredWindowWidth(graph2D);
                    window.Height = CountDesiredWindowHeight(graph2D);
                }
            }
        }

        private static int CountDesiredWindowWidth(Graph2D graph)
        {
            return (graph.Width + WidthOffset) * DistanceBetweenVertices;
        }

        private static int CountDesiredWindowHeight(Graph2D graph)
        {
            return (graph.Length + LengthOffset) * DistanceBetweenVertices;
        }
    }
}
