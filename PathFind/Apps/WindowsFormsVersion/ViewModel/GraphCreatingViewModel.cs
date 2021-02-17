using Common.Interfaces;
using GraphLib.Interface;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using System;

namespace WindowsFormsVersion.ViewModel
{
    internal class GraphCreatingViewModel : GraphCreatingModel, IViewModel
    {
        public event EventHandler OnWindowClosed;

        public GraphCreatingViewModel(IMainModel model,
            IGraphAssembler graphFactory) : base(model, graphFactory)
        {
            
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
