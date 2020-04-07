using System.Windows.Forms;
using GraphLibrary.GraphFactory;
using GraphLibrary.GraphLoader;
using SearchAlgorythms.Graph;
using SearchAlgorythms.GraphFactory;

namespace SearchAlgorythms.GraphLoader
{
    public class WinFormsGraphLoader : AbstractGraphLoader
    {
        private readonly int placeBetweenButtons;

        public WinFormsGraphLoader(int placeBetweenButtons) => this.placeBetweenButtons = placeBetweenButtons;

        public override AbstractGraph CreateGraph(AbstractGraphInitializer initializer) => initializer.GetGraph();

        public override AbstractGraphInitializer GetInitializer(VertexInfo[,] info) 
            => new WinFormsGraphInitializer(info, placeBetweenButtons);

        public override string GetPath()
        {
            var open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
                return open.FileName;
            else
                return "";
        }

        public override void ShowMessage(string message) => MessageBox.Show(message);
    }
}
