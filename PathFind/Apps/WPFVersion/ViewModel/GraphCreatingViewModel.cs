using Common.Interface;
using GraphLib.Interface;
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

        public GraphCreatingViewModel(IMainModel model,
            IGraphAssembler graphFactory) : base(model, graphFactory)
        {
            ConfirmCreateGraphCommand = new RelayCommand(ExecuteConfirmCreateGraphCommand);
            CancelCreateGraphCommand = new RelayCommand(obj => CloseWindow());
        }

        private void ExecuteConfirmCreateGraphCommand(object param)
        {
            CreateGraph();

            CloseWindow();
            WindowService.Adjust(model.Graph);
        }

        private void CloseWindow()
        {
            OnWindowClosed?.Invoke(this, new EventArgs());
        }
    }
}
