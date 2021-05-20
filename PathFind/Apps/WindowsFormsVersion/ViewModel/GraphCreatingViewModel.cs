using AssembleClassesLib.Interface;
using Common.Interface;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using Logging.Interface;
using System;

namespace WindowsFormsVersion.ViewModel
{
    internal class GraphCreatingViewModel : GraphCreatingModel, IViewModel
    {
        public event EventHandler OnWindowClosed;

        public GraphCreatingViewModel(ILog log, IMainModel model,
            IAssembleClasses graphFactories)
            : base(log, model, graphFactories)
        {

        }

        public void CreateGraph(object sender, EventArgs e)
        {
            if (CanExecuteConfirmGraphAssembleChoice())
            {
                CreateGraph();
                OnWindowClosed?.Invoke(this, EventArgs.Empty);
                OnWindowClosed = null;
            }
        }

        public void CancelCreateGraph(object sender, EventArgs e)
        {
            OnWindowClosed?.Invoke(this, EventArgs.Empty);
            OnWindowClosed = null;
        }

        private bool CanExecuteConfirmGraphAssembleChoice()
        {
            return GraphAssembleKeys.Contains(GraphAssembleKey)
                && Constants.GraphParamsValueRanges.Contains(GraphParametres);
        }
    }
}
