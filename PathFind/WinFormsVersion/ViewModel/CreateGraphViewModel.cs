using GraphLibrary.Common.Constants;
using GraphLibrary.GraphFactory;
using GraphLibrary.Model;
using System;
using WinFormsVersion.GraphFactory;
using WinFormsVersion.Model;

namespace WinFormsVersion.ViewModel
{
    internal class CreateGraphViewModel : AbstractCreateGraphModel
    {
        public CreateGraphViewModel(IMainModel model) : base(model)
        {
            vertexEventSetter = new WinFormsVertexEventSetter();
            graphFieldFiller = new WinFormsGraphFieldFiller();
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
            if (int.TryParse(Width, out int width) && int.TryParse(Height, out int height))
                return GraphParametresRange.
                    IsValidGraphParamters(width, height, ObstaclePercent);
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
            return new WinFormsGraphFactory(ObstaclePercent,
                width, height, VertexSize.SIZE_BETWEEN_VERTICES);
        }
    }
}
