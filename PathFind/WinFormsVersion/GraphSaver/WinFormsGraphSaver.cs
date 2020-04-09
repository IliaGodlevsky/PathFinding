using System.Windows.Forms;
using GraphLibrary.GraphSaver;

namespace SearchAlgorythms.GraphSaver
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
