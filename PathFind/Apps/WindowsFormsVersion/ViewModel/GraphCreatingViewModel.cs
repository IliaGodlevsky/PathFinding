using Common.Interface;
using GraphLib.Interfaces.Factories;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using System;

namespace WindowsFormsVersion.ViewModel
{
    internal class GraphCreatingViewModel : GraphCreatingModel, IViewModel
    {
        public event EventHandler OnWindowClosed;

        public GraphCreatingViewModel(ILog log, IMainModel model, IGraphAssemble graphFactory)
            : base(log, model, graphFactory)
        {

        }

        public void CreateGraph(object sender, EventArgs e)
        {
            CreateGraph();
            OnWindowClosed?.Invoke(this, EventArgs.Empty);
            OnWindowClosed = null;
        }

        public void CancelCreateGraph(object sender, EventArgs e)
        {
            OnWindowClosed?.Invoke(this, EventArgs.Empty);
            OnWindowClosed = null;
        }
    }
}
