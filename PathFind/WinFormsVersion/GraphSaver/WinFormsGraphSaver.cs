using System.Windows.Forms;
using GraphLibrary.GraphSaver;

namespace WinFormsVersion.GraphSaver
{
    public class WinFormsGraphSaver : AbstractGraphSaver
    {
        protected override string GetPath()
        {
            var save = new SaveFileDialog();
            return save.ShowDialog() == DialogResult.OK ? save.FileName : "";
        }
    }
}
