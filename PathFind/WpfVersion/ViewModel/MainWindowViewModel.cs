using GraphLibrary.Common.Constants;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfVersion.Infrastructure;
using WpfVersion.Model;
using WpfVersion.Model.GraphLoader;
using WpfVersion.Resources;
using WpfVersion.View.Windows;
using WpfVersion.Model.EventHolder;
using GraphLibrary.Graphs;
using GraphLibrary.Vertex;
using GraphLibrary.ViewModel.Interface;
using GraphLibrary.ViewModel;
using GraphLibrary.GraphField;
using GraphLibrary.GraphSerialization.GraphSaver;
using Microsoft.Win32;

namespace WpfVersion.ViewModel
{
    internal class MainWindowViewModel : AbstractMainModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Window Window { get; set; }

        private string graphParametres;
        public override string GraphParametres
        {
            get { return graphParametres; }
            set { graphParametres = value; OnPropertyChanged(); }
        }

        private string statistics;
        public override string Statistics 
        { 
            get{return statistics;}
            set{statistics = value; OnPropertyChanged();}
        }

        private IGraphField graphField;
        public override IGraphField GraphField 
        { 
            get { return graphField; } 
            set { graphField = value; OnPropertyChanged(); } 
        }

        private Graph graph;
        public override Graph Graph 
        { 
            get { return graph; } 
            set { graph = value; OnPropertyChanged(); } 
        }

        public RelayCommand StartPathFindCommand { get; }
        public RelayCommand CreateNewGraphCommand { get; }
        public RelayCommand ClearGraphCommand { get; }
        public RelayCommand SaveGraphCommand { get; }
        public RelayCommand LoadGraphCommand { get; }

        public MainWindowViewModel()
        {
            GraphField = new WpfGraphField();
            GraphParametresFormat = WpfVersionResources.GraphParametresFormat;
            saver = new GraphSaver();
            loader = new WpfGraphLoader(VertexSize.SIZE_BETWEEN_VERTICES);
            VertexEventHolder = new WpfVertexEventHolder();
            graphFieldFiller = new WpfGraphFieldFiller();

            StartPathFindCommand = new RelayCommand(
                ExecuteStartPathFindCommand,
               CanExecuteStartFindPathCommand);
            CreateNewGraphCommand = new RelayCommand(ExecuteCreateNewGraphCommand, obj => true);
            ClearGraphCommand = new RelayCommand(ExecuteClearGraphCommand, obj=> Graph != null);
            SaveGraphCommand = new RelayCommand(ExecuteSaveGraphCommand, obj => Graph != null);
            LoadGraphCommand = new RelayCommand(ExecuteLoadGraphCommand, obj => true);
        }

        public override void FindPath()
        {
            PrepareWindow(new PathFindViewModel(this), new PathFindWindow());
        }

        public override void CreateNewGraph()
        {
            PrepareWindow(new GraphCreateViewModel(this), new GraphCreatesWindow());
        }

        public void Dispose()
        {
            OnDispose();
        }

        private void ExecuteSaveGraphCommand(object param)
        {
            base.SaveGraph();
        }

        private bool CanExecuteStartFindPathCommand(object param)
        {
            return Graph?.End != NullVertex.Instance 
                && Graph?.Start != NullVertex.Instance && Graph != null;
        }

        private void ExecuteLoadGraphCommand(object param)
        {
            base.LoadGraph();
            OnPropertyChanged(nameof(GraphField));
            OnPropertyChanged(nameof(Graph));
            OnPropertyChanged(nameof(GraphParametres));
            WindowAdjust.Adjust(Graph);
        }

        private void ExecuteClearGraphCommand(object param)
        {
            base.ClearGraph();
            OnPropertyChanged(nameof(GraphParametres));
        }

        private void ExecuteStartPathFindCommand(object param)
        {
            FindPath();
        }

        private void ExecuteCreateNewGraphCommand(object param)
        {
            CreateNewGraph();
        }

        protected virtual void OnDispose()
        {
            return;
        }

        

        private void PrepareWindow(IModel model, Window window)
        {
            Window = window;
            Window.DataContext = model;
            Window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Window.Show();
        }

        protected override string GetSavePath()
        {
            var save = new SaveFileDialog();
            return save.ShowDialog() == true ? save.FileName : string.Empty;
        }

        protected override string GetLoadPath()
        {
            var open = new OpenFileDialog();
            return open.ShowDialog() == true ? open.FileName : string.Empty;
        }
    }
}
