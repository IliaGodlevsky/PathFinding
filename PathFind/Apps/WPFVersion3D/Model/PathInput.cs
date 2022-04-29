using GraphLib.Serialization.Interfaces;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Threading;

namespace WPFVersion3D.Model
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
            return dialog.ShowDialog() == true ? dialog.FileName : string.Empty;
        }
    }
}
