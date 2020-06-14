using GraphFactory.GraphSaver;
using GraphLibrary.Constants;
using GraphLibrary.GraphLoader;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using WpfVersion.Infrastructure;
using WpfVersion.Model.Graph;
using WpfVersion.Model.GraphLoader;
using WpfVersion.Model.GraphSaver;
using WpfVersion.View.Windows;

namespace WpfVersion.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string statistics;

        public string Statistics 
        { 
            get{return statistics;}
            set{statistics = value; OnPropertyChanged();}
        }

        public Window Window { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private Canvas graphField;
        public Canvas GraphField { get { return graphField; } set { graphField = value; OnPropertyChanged(); } }

        private WpfGraph graph;
        public WpfGraph Graph { get { return graph; } set { graph = value; OnPropertyChanged(); } }

        public RelayCommand StartPathFindCommand { get; }
        public RelayCommand CreateNewGraphCommand { get; }
        public RelayCommand ClearGraphCommand { get; }
        public RelayCommand SaveGraphCommand { get; }
        public RelayCommand LoadGraphCommand { get; }
        public MainWindowViewModel()
        {
            GraphField = new Canvas();
            StartPathFindCommand = new RelayCommand(ExecuteStartPathFindCommand, obj => Graph?.End != null && Graph?.Start != null);
            CreateNewGraphCommand = new RelayCommand(ExecuteCreateNewGraphCommand, obj => true);
            ClearGraphCommand = new RelayCommand(ExecuteClearGraphCommand, obj=> Graph != null);
            SaveGraphCommand = new RelayCommand(ExecuteSaveGraphCommand, obj => Graph != null);
            LoadGraphCommand = new RelayCommand(ExecuteLoadGraphCommand, obj => true);
        }

        private void ExecuteSaveGraphCommand(object param)
        {
            IGraphSaver saver = new WpfGraphSaver();
            saver.SaveGraph(Graph);
        }

        private void ExecuteLoadGraphCommand(object param)
        {
            IGraphLoader loader = new WpfGraphLoader(Const.SIZE_BETWEEN_VERTICES);
            graph = (WpfGraph)loader.GetGraph();
            GraphCreateViewModel.FillGraphField(ref graph, ref graphField);
            OnPropertyChanged(nameof(GraphField));
        }


        private void ExecuteClearGraphCommand(object param)
        {
            Graph.Refresh();
            Statistics = string.Empty;
        }

        private void ExecuteStartPathFindCommand(object param)
        {
            PathFindViewModel model = new PathFindViewModel(this);
            Window = new PathFindWindow();
            Window.DataContext = model;
            Window.Show();
        }

        private void ExecuteCreateNewGraphCommand(object param)
        {
            Window = new GraphCreatesWindow();
            GraphCreateViewModel model = new GraphCreateViewModel(this);
            Window.DataContext = model;
            Window.Show();
        }

        public virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            OnDispose();
        }

        protected virtual void OnDispose()
        {
            return;
        }
    }
}
