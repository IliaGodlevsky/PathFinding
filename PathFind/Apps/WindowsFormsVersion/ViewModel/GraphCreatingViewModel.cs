using Common.Interface;
using GraphLib.Interfaces.Factories;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using Logging.Interface;
using System;
using System.Collections.Generic;

namespace WindowsFormsVersion.ViewModel
{
    internal class GraphCreatingViewModel : GraphCreatingModel, IModel, IViewModel
    {
        public event EventHandler WindowClosed;

        public GraphCreatingViewModel(ILog log, IMainModel model,
            IEnumerable<IGraphAssemble> graphAssembles)
            : base(log, model, graphAssembles)
        {

        }

        public void CreateGraph(object sender, EventArgs e)
        {
            if (CanExecuteConfirmGraphAssembleChoice())
            {
                CreateGraph();
                WindowClosed?.Invoke(this, EventArgs.Empty);
                WindowClosed = null;
            }
        }

        public void CancelCreateGraph(object sender, EventArgs e)
        {
            WindowClosed?.Invoke(this, EventArgs.Empty);
            WindowClosed = null;
        }

        private bool CanExecuteConfirmGraphAssembleChoice()
        {
            return SelectedGraphAssemble != null
                && Constants.GraphWidthValueRange.Contains(Width)
                && Constants.GraphLengthValueRange.Contains(Length);
        }
    }
}
