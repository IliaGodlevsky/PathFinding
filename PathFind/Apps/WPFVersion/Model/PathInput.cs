using GraphLib.Serialization.Interfaces;
using Microsoft.Win32;

namespace WPFVersion.Model
{
    internal sealed class PathInput : IPathInput
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
            return dialog?.ShowDialog() == true
                ? dialog.FileName
                : string.Empty;
        }
    }
}
