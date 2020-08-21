using GraphLibrary.Constants;
using GraphLibrary.GraphFactory;
using GraphLibrary.Model;
using System.Windows;
using WpfVersion.Infrastructure;
using WpfVersion.Model;
using WpfVersion.Model.GraphFactory;

namespace WpfVersion.ViewModel
{
    public class GraphCreateViewModel : AbstractCreateGraphModel
    {

        public RelayCommand ConfirmCreateGraphCommand { get; }
        public RelayCommand CancelCreateGraphCommand { get; }

        public GraphCreateViewModel(IMainModel model) : base(model)
        {
            this.model = model;
            graphField = model.GraphField;
            filler = new WpfGraphFiller();
            ConfirmCreateGraphCommand = new RelayCommand(
                ExecuteConfirmCreateGraphCommand, 
                CanExecuteConfirmCreateGraphCommand);
            CancelCreateGraphCommand = new RelayCommand(obj=> 
            (model as MainWindowViewModel)?.Window.Close(), obj => true);
        }

        private void ExecuteConfirmCreateGraphCommand(object param)
        {
            base.CreateGraph();
            (model as MainWindowViewModel).Window.Close();
            Application.Current.MainWindow.Width = (graph.Width + 1) * 
                Const.SIZE_BETWEEN_VERTICES + Const.SIZE_BETWEEN_VERTICES;
            Application.Current.MainWindow.Height = (1 + graph.Height) * 
                Const.SIZE_BETWEEN_VERTICES +
                Application.Current.MainWindow.DesiredSize.Height;
        }

        private bool CanExecuteConfirmCreateGraphCommand(object param)
        {
            if (int.TryParse(Width, out int width)
                && int.TryParse(Height, out int height))
                return width > 0 && height > 0;
            else
                return false;
        }

        public override IGraphFactory GetFactory()
        {
            int width = int.Parse(Width);
            int height = int.Parse(Height);
            return new RandomValuedWpfGraphFactory(ObstaclePercent,
                width, height, Const.SIZE_BETWEEN_VERTICES);
        }
    }
}
