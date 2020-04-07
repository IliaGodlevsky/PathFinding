using System.Windows.Forms;
using GraphLibrary.GraphSaver;

namespace SearchAlgorythms.GraphSaver
{
    public class WinFormsGraphSaver : AbstractGraphSaver
    {
        public override string GetPath()
        {
            var save = new SaveFileDialog();
            if (save.ShowDialog() == DialogResult.OK)
                return save.FileName;
            else
                return "";
        }
    }
}
