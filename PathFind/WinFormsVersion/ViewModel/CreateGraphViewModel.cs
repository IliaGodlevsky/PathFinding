using GraphLibrary.Constants;
using GraphLibrary.GraphFactory;
using GraphLibrary.ViewModel;
using GraphLibrary.ViewModel.Interface;
using System;
using WinFormsVersion.GraphFactory;
using WinFormsVersion.Model;

namespace WinFormsVersion.ViewModel
{
    internal class CreateGraphViewModel : AbstractCreateGraphModel
    {
        public CreateGraphViewModel(IMainModel model) : base(model)
        {            
            graphFieldFiller = new WinFormsGraphFieldFiller();
        }

        public override void CreateGraph()
        {
            base.CreateGraph();
            (model as MainWindowViewModel).Window?.Close();
        }

        public void CreateGraph(object sender, EventArgs e)
        {         
            CreateGraph();
        }

        public void CancelCreateGraph(object sender, EventArgs e)
        {
            (model as MainWindowViewModel)?.Window.Close();
        }

        public override IGraphFactory GetFactory()
        {
            return new WinFormsGraphFactory(ObstaclePercent,
                Width, Height, VertexSize.SIZE_BETWEEN_VERTICES);
        }
    }
}
