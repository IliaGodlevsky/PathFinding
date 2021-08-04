using Common.Interface;
using GraphLib.Interfaces.Factories;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using Logging.Interface;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using WPFVersion.Infrastructure;
using WPFVersion.Model;

namespace WPFVersion.ViewModel
{
    internal class GraphCreatingViewModel : GraphCreatingModel, IViewModel
    {
        public event EventHandler WindowClosed;

        public ICommand ConfirmCreateGraphCommand { get; }
        public ICommand CancelCreateGraphCommand { get; }

        public GraphCreatingViewModel(ILog log, IMainModel model,
            IEnumerable<IGraphAssemble> graphAssembles)
            : base(log, model, graphAssembles)
        {
            ConfirmCreateGraphCommand = new RelayCommand(ExecuteConfirmCreateGraphCommand,
                CanExecuteConfirmCreateGraphCommand);
            CancelCreateGraphCommand = new RelayCommand(ExecuteCloseWindowCommand);
        }

        private void ExecuteConfirmCreateGraphCommand(object param)
        {
            CreateGraph();

            ExecuteCloseWindowCommand(null);
            WindowService.Adjust(model.Graph);
        }

        private void ExecuteCloseWindowCommand(object param)
        {
            WindowClosed?.Invoke(this, EventArgs.Empty);
            WindowClosed = null;
        }

        private bool CanExecuteConfirmCreateGraphCommand(object sender)
        {
            return SelectedGraphAssemble != null
                && Constants.GraphWidthValueRange.Contains(Width)
                && Constants.GraphLengthValueRange.Contains(Length);
        }
    }
}
