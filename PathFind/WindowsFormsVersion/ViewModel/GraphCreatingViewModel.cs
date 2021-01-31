using Common.Interfaces;
using Common.ValueRanges;
using GraphLib.Graphs.Factories.Interfaces;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using System;
using System.Configuration;

namespace WindowsFormsVersion.ViewModel
{
    internal class GraphCreatingViewModel : GraphCreatingModel, IViewModel
    {
        public event EventHandler OnWindowClosed;

        public GraphCreatingViewModel(IMainModel model,
            IGraphFiller graphFactory) : base(model, graphFactory)
        {
            int upperRangeOfGraphWidth
                = Convert.ToInt32(ConfigurationManager.AppSettings["upperRangeOfGraphWidth"]);
            int upperRangeOfGraphLength
                = Convert.ToInt32(ConfigurationManager.AppSettings["upperRangeOfGraphLength"]);

            GraphWidthValueRange = new ValueRange(upperRangeOfGraphWidth, 0);
            GraphLengthValueRange = new ValueRange(upperRangeOfGraphLength, 0);
        }

        public void CreateGraph(object sender, EventArgs e)
        {
            CreateGraph();
            OnWindowClosed?.Invoke(this, new EventArgs());
            OnWindowClosed = null;
        }

        public void CancelCreateGraph(object sender, EventArgs e)
        {
            OnWindowClosed?.Invoke(this, new EventArgs());
            OnWindowClosed = null;
        }
    }
}
