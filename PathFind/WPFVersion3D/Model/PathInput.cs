using GraphLib.Graphs.Serialization.Interfaces;
using Microsoft.Win32;

namespace WPFVersion3D.Model
{
    internal class PathInput : IPathInput
    {
        public string InputLoadPath()
        {
            return InputPath(new OpenFileDialog());
        }

        public string InputSavePath()
        {
            return InputPath(new SaveFileDialog());
        }

        private string InputPath(FileDialog dialog)
        {
            return dialog.ShowDialog() == true ? dialog.FileName : string.Empty;
        }
    }
}
