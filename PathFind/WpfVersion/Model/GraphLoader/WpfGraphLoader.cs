using GraphLibrary;
using GraphLibrary.GraphFactory;
using GraphLibrary.GraphLoader;
using Microsoft.Win32;
using System.Windows;
using WpfVersion.Model.GraphFactory;

namespace WpfVersion.Model.GraphLoader
{
    public class WpfGraphLoader : AbstractGraphLoader
    {
        private readonly int placeBetweenButtons;

        public WpfGraphLoader(int placeBetweenButtons) => this.placeBetweenButtons = placeBetweenButtons;

        protected override AbstractGraphInitializer GetInitializer(VertexInfo[,] info)
            => new WpfGraphInitializer(info, placeBetweenButtons);

        protected override string GetPath()
        {
            var open = new OpenFileDialog();
            return open.ShowDialog() == true ? open.FileName : "";
        }

        protected override void ShowMessage(string message) => MessageBox.Show(message);
    }
}
