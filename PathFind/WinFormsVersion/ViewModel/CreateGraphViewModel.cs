using GraphLibrary.Constants;
using GraphLibrary.GraphFactory;
using GraphLibrary.Model;
using System;
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
            base.CreateGraph();
            (model as MainWindowViewModel).Window?.Close();
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
