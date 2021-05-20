using AssembleClassesLib.Interface;
using Common.Interface;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using Logging.Interface;
using System;
using System.Windows.Input;
using WPFVersion.Infrastructure;
using WPFVersion.Model;

namespace WPFVersion.ViewModel
{
    internal class GraphCreatingViewModel : GraphCreatingModel, IViewModel
    {
        public event EventHandler OnWindowClosed;

        public ICommand ConfirmCreateGraphCommand { get; }
        public ICommand CancelCreateGraphCommand { get; }

        public GraphCreatingViewModel(ILog log, IMainModel model, IAssembleClasses graphFactories)
            : base(log, model, graphFactories)
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
            OnWindowClosed?.Invoke(this, EventArgs.Empty);
            OnWindowClosed = null;
        }

        private bool CanExecuteConfirmCreateGraphCommand(object sender)
        {
            return GraphAssembleKeys.Contains(GraphAssembleKey)
                && Constants.GraphParamsValueRanges.Contains(GraphParametres);
        }
    }
}
