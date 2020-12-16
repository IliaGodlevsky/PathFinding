using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using WpfVersion.Infrastructure;
using WpfVersion.Model;
using WpfVersion.Model.Vertex;

namespace WpfVersion.ViewModel
{
    internal class GraphCreatingViewModel : GraphCreatingModel
    {
        public RelayCommand ConfirmCreateGraphCommand { get; }

        public RelayCommand CancelCreateGraphCommand { get; }

        public GraphCreatingViewModel(IMainModel model) : base(model)
        {
            ConfirmCreateGraphCommand = new RelayCommand(ExecuteConfirmCreateGraphCommand, obj => true);
            CancelCreateGraphCommand = new RelayCommand(obj => CloseWindow(), obj => true);
        }

        private void ExecuteConfirmCreateGraphCommand(object param)
        {
            base.CreateGraph(() => new WpfVertex());

            CloseWindow();
            WindowAdjust.Adjust(model.Graph);
        }

        private void CloseWindow()
        {
            (model as MainWindowViewModel)?.Window.Close();
        }
    }
}
