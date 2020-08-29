using GraphLibrary;
using GraphLibrary.GraphFactory;
using GraphLibrary.GraphLoader;
using Microsoft.Win32;
using System.Windows;
using WpfVersion.Model.GraphFactory;

namespace WpfVersion.Model.GraphLoader
{
    internal class WpfGraphLoader : AbstractGraphLoader
    {
        private readonly int placeBetweenButtons;

        public WpfGraphLoader(int placeBetweenButtons) => this.placeBetweenButtons = placeBetweenButtons;

        protected override AbstractGraphInfoInitializer GetInitializer(VertexInfo[,] info)
            => new WpfGraphInitializer(info, placeBetweenButtons);

        protected override string GetPath()
        {
            var open = new OpenFileDialog();
            return open.ShowDialog() == true ? open.FileName : string.Empty;
        }

        protected override void ShowMessage(string message) => MessageBox.Show(message);
    }
}
