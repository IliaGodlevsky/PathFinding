using GraphLibrary.GraphSaver;
using Microsoft.Win32;
using System.Windows;

namespace WpfVersion.Model.GraphSaver
{
    internal class WpfGraphSaver : AbstractGraphSaver
    {
        protected override string GetPath()
        {
            var save = new SaveFileDialog();
            return save.ShowDialog() == true ? save.FileName : string.Empty;
        }

        protected override void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
