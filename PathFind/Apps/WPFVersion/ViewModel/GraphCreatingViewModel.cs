using Common.Interface;
using GraphLib.Interfaces.Factories;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
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

        public GraphCreatingViewModel(IMainModel model, IGraphAssembler graphFactory)
            : base(model, graphFactory)
        {
            ConfirmCreateGraphCommand = new RelayCommand(ExecuteConfirmCreateGraphCommand);
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
        }
    }
}
