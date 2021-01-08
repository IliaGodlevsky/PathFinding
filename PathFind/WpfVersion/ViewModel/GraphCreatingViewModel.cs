using Common.Interfaces;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using System;
using WpfVersion.Infrastructure;
using WpfVersion.Model;
using WpfVersion.Model.Vertex;

namespace WpfVersion.ViewModel
{
    internal class GraphCreatingViewModel : GraphCreatingModel, IViewModel
    {
        public event EventHandler OnWindowClosed;

        public RelayCommand ConfirmCreateGraphCommand { get; }

        public RelayCommand CancelCreateGraphCommand { get; }

        public GraphCreatingViewModel(IMainModel model) : base(model)
        {
            ConfirmCreateGraphCommand = new RelayCommand(ExecuteConfirmCreateGraphCommand);
            CancelCreateGraphCommand = new RelayCommand(obj => CloseWindow());
        }

        private void ExecuteConfirmCreateGraphCommand(object param)
        {
            base.CreateGraph(() => new WpfVertex());

            CloseWindow();
            WindowService.Adjust(model.Graph);
        }

        private void CloseWindow()
        {
            OnWindowClosed?.Invoke(this, new EventArgs());
        }
    }
}
