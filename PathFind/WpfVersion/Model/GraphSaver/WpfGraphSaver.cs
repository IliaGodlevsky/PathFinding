using GraphLibrary.GraphSaver;
using Microsoft.Win32;

namespace WpfVersion.Model.GraphSaver
{
    public class WpfGraphSaver : AbstractGraphSaver
    {
        protected override string GetPath()
        {
            var save = new SaveFileDialog();
            return save.ShowDialog() == true ? save.FileName : "";
        }
    }
}
