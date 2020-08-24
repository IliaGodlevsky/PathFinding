using GraphLibrary.Constants;
using GraphLibrary.GraphFactory;
using GraphLibrary.Model;
using System;
using System.Drawing;
using WinFormsVersion.GraphFactory;
using WinFormsVersion.Model;

namespace WinFormsVersion.ViewModel
{
    public class CreateGraphViewModel : AbstractCreateGraphModel
    {
        public CreateGraphViewModel(IMainModel model) : base(model)
        {
            filler = new WinFormsGraphFiller();
        }

        public override void CreateGraph()
        {
            int size = Const.SIZE_BETWEEN_VERTICES;
            var window = (model as MainWindowViewModel).MainWindow;
            var field = window.GraphField;
            window.Controls.Remove(field);
            base.CreateGraph();
            (model as MainWindowViewModel).Window?.Close();
            (graphField as WinFormsGraphField).Size = new Size(model.Graph.Width * size, model.Graph.Height * size);
            window.Controls.Add(graphField as WinFormsGraphField);
        }

        public void CreateGraph(object sender, EventArgs e)
        {
            if (!CanCreate())
                return;           
            CreateGraph();            
        }

        private bool CanCreate()
        {
            if (int.TryParse(Width, out int width)
                && int.TryParse(Height, out int height))
                return width > 0 && height > 0;
            else
                return false;
        }

        public void CancelCreateGraph(object sender, EventArgs e)
        {
            (model as MainWindowViewModel)?.Window.Close();
        }

        public override IGraphFactory GetFactory()
        {
            int width = int.Parse(Width);
            int height = int.Parse(Height);
            return new RandomValuedWinFormsGraphFactory(ObstaclePercent,
                width, height, Const.SIZE_BETWEEN_VERTICES);
        }
    }
}
