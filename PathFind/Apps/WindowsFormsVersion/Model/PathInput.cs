using GraphLib.Serialization.Interfaces;
using System.Windows.Forms;

namespace WindowsFormsVersion.Model
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

        private string InputPath(FileDialog fileDialog)
        {
            return fileDialog.ShowDialog() == DialogResult.OK ? fileDialog.FileName : string.Empty;
        }
    }
}
