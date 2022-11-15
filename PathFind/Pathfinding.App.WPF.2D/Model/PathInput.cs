using Microsoft.Win32;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using System.Windows;
using System.Windows.Threading;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class PathInput : IPathInput
    {
        private static Dispatcher Dispatcher => Application.Current.Dispatcher;

        public string InputLoadPath()
        {
            return Dispatcher.Invoke(() => InputPath(new OpenFileDialog()));
        }

        public string InputSavePath()
        {
            return Dispatcher.Invoke(() => InputPath(new SaveFileDialog()));
        }

        private string InputPath(FileDialog dialog)
        {
            return dialog?.ShowDialog() == true
                ? dialog.FileName
                : string.Empty;
        }
    }
}
