using GraphLibrary.Constants;
using GraphLibrary.GraphFactory;
using GraphLibrary.Model;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsVersion.GraphFactory;
using WinFormsVersion.Model;
using WinFormsVersion.Vertex;

namespace WinFormsVersion.ViewModel
{
    public class CreateGraphViewModel : AbstractCreateGraphModel
    {
        public CreateGraphViewModel(IMainModel model) : base(model)
        {
            this.model = model;
            graphField = model.GraphField;
            filler = new WinFormsGraphFiller();
        }
        public override void CreateGraph()
        {
            int size = Const.SIZE_BETWEEN_VERTICES;
            MainWindowViewModel mainModel = (model as MainWindowViewModel);
            var field = mainModel.MainWindow.GraphField;
            mainModel.MainWindow.Controls.Remove(field);
            base.CreateGraph();
            mainModel?.Window.Close();
            mainModel.MainWindow.GraphField = new WinFormsGraphField() { Location = new Point(4, 90) };
            mainModel.MainWindow.GraphField.Controls.AddRange(model.Graph.GetArray().
                Cast<WinFormsVertex>().ToArray());
            mainModel.MainWindow.GraphField.Size = new Size(model.Graph.Width * size, model.Graph.Height * size);
            mainModel.MainWindow.Controls.Add(mainModel.MainWindow.GraphField);
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
