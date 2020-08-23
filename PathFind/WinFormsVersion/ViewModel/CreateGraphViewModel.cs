using GraphLibrary.Constants;
using GraphLibrary.GraphFactory;
using GraphLibrary.Model;
using System;
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

        public void CreateGraph(object sender, EventArgs e)
        {
            (model as MainWindowViewModel).MainWindow.GraphField.Controls.Clear();
            (graphField as UserControl).Controls.Clear();
            base.CreateGraph();
            (model as MainWindowViewModel)?.Window.Close();
            (model as MainWindowViewModel).MainWindow.GraphField.Controls.AddRange(
                model.Graph.GetArray().Cast<WinFormsVertex>().ToArray());
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
