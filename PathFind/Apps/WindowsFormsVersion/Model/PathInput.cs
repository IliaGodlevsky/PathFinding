using GraphLib.Serialization.Interfaces;
using System;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsVersion.Model
{
    internal sealed class PathInput : IPathInput
    {
        public string InputLoadPath()
        {
            return InputPath(() => new OpenFileDialog());
        }

        public string InputSavePath()
        {
            return InputPath(() => new SaveFileDialog());
        }

        private string InputPath(Func<FileDialog> dialogFactory)
        {
            string path = string.Empty;
            var thread = new Thread(() => path = InputPath(dialogFactory()));        
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            return path;
        }

        private string InputPath(FileDialog fileDialog)
        {
            return fileDialog?.ShowDialog() == DialogResult.OK ? fileDialog.FileName : string.Empty;
        }
    }
}
