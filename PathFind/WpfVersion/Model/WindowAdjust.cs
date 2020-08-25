using GraphLibrary.Constants;
using GraphLibrary.Graph;
using System.Windows;

namespace WpfVersion.Model
{
    public static class WindowAdjust
    {
        public static void Adjust(AbstractGraph graph)
        {
            if (graph == null)
                return;
            Application.Current.MainWindow.Width = (graph.Width + 1) * Const.SIZE_BETWEEN_VERTICES + Const.SIZE_BETWEEN_VERTICES;
            Application.Current.MainWindow.Height = (1 + graph.Height) * Const.SIZE_BETWEEN_VERTICES +
                Application.Current.MainWindow.DesiredSize.Height;
        }
    }
}
