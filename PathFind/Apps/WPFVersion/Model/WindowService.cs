using Common.Extensions;
using GraphLib.Interface;
using GraphLib.Realizations;
using System.Windows;

using static WPFVersion.Constants;

namespace WPFVersion.Model
{
    internal static class WindowService
    {
        private const int WidthOffset = 2;
        private const int LengthOffset = 6;

        public static void Adjust(IGraph graph)
        {
            if (!graph.IsDefault())
            {
                int distanceBetweenVertices = DistanceBetweenVertices + VertexSize;

                if (graph is Graph2D graph2d)
                {
                    var mainWindowDesiredWidth = (graph2d.Width + WidthOffset) * distanceBetweenVertices;
                    var mainWindowDesiredHeight = (graph2d.Length + LengthOffset) * distanceBetweenVertices;

                    Application.Current.MainWindow.Width = mainWindowDesiredWidth;
                    Application.Current.MainWindow.Height = mainWindowDesiredHeight;
                }
            }
        }
    }
}
