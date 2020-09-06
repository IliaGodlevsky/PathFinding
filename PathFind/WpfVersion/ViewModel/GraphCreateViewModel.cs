using GraphLibrary.Common.Constants;
using GraphLibrary.GraphFactory;
using GraphLibrary.Model;
using WpfVersion.Infrastructure;
using WpfVersion.Model;
using WpfVersion.Model.GraphFactory;

namespace WpfVersion.ViewModel
{
    internal class GraphCreateViewModel : AbstractCreateGraphModel
    {
        public RelayCommand ConfirmCreateGraphCommand { get; }
        public RelayCommand CancelCreateGraphCommand { get; }

        public GraphCreateViewModel(IMainModel model) : base(model)
        {
            this.model = model;
            graphField = model.GraphField;
            graphFieldFiller = new WpfGraphFieldFiller();
            ConfirmCreateGraphCommand = new RelayCommand(
                ExecuteConfirmCreateGraphCommand, obj => true);
            CancelCreateGraphCommand = new RelayCommand(obj=> 
            (model as MainWindowViewModel)?.Window.Close(), obj => true);
        }

        private void ExecuteConfirmCreateGraphCommand(object param)
        {
            base.CreateGraph();
            (model as MainWindowViewModel).Window.Close();
            WindowAdjust.Adjust(model.Graph);
        }

        public override IGraphFactory GetFactory()
        {
            return new WpfGraphFactory(ObstaclePercent,
                Width, Height, VertexSize.SIZE_BETWEEN_VERTICES);
        }
    }
}
