using GraphLibrary.Constants;
using GraphLibrary.GraphCreate.GraphFactory.Interface;
using GraphLibrary.GraphFactory;
using GraphLibrary.ViewModel;
using GraphLibrary.ViewModel.Interface;
using WpfVersion.Infrastructure;
using WpfVersion.Model;
using WpfVersion.Model.Vertex;

namespace WpfVersion.ViewModel
{
    internal class GraphCreateViewModel : AbstractCreateGraphModel
    {
        public RelayCommand ConfirmCreateGraphCommand { get; }
        public RelayCommand CancelCreateGraphCommand { get; }

        public GraphCreateViewModel(IMainModel model) : base(model)
        {
            ConfirmCreateGraphCommand = new RelayCommand(
                ExecuteConfirmCreateGraphCommand, obj => true);
            CancelCreateGraphCommand = new RelayCommand(obj=> 
            (model as MainWindowViewModel)?.Window.Close(), obj => true);
        }

        private void ExecuteConfirmCreateGraphCommand(object param)
        {
            base.CreateGraph(() => new WpfVertex());
            (model as MainWindowViewModel).Window.Close();
            WindowAdjust.Adjust(model.Graph);
        }

        public override IGraphFactory GetFactory()
        {
            return new GraphFactory(ObstaclePercent,
                Width, Height, VertexSize.SIZE_BETWEEN_VERTICES);
        }
    }
}
