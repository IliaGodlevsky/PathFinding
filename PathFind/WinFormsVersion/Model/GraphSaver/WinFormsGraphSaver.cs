using System.Windows.Forms;
using GraphLibrary.GraphSaver;

namespace WinFormsVersion.GraphSaver
{
    internal class WinFormsGraphSaver : AbstractGraphSaver
    {
        protected override string GetPath()
        {
            var save = new SaveFileDialog();
            return save.ShowDialog() == DialogResult.OK ? save.FileName : string.Empty;
        }

        protected override void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
