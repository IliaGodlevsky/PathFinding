using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfVersion.Infrastructure;
using WpfVersion.Model;
using WpfVersion.Resources;
using WpfVersion.View.Windows;
using WpfVersion.Model.EventHolder;
using GraphLibrary.Graphs;
using GraphLibrary.Vertex;
using GraphLibrary.ViewModel.Interface;
using GraphLibrary.ViewModel;
using GraphLibrary.GraphField;
using Microsoft.Win32;
using WpfVersion.Model.Vertex;
using GraphLibrary.Graphs.Interface;
using System.Linq;

namespace WpfVersion.ViewModel
{
    internal class MainWindowViewModel : MainModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        private IGraph graph;
        public override IGraph Graph 
        { 
            get { return graph; } 
            protected set { graph = value; OnPropertyChanged(); } 
        }

        public RelayCommand StartPathFindCommand { get; }
        public RelayCommand CreateNewGraphCommand { get; }
        public RelayCommand ClearGraphCommand { get; }
        public RelayCommand SaveGraphCommand { get; }
        public RelayCommand LoadGraphCommand { get; }
        public RelayCommand ChangeVertexSize { get; }

        public MainWindowViewModel()
        {
            GraphField = new WpfGraphField();
            GraphParametresFormat = WpfVersionResources.GraphParametresFormat;
            VertexEventHolder = new WpfVertexEventHolder();
            FieldFactory = new WpfGraphFieldFactory();
            DtoConverter = (dto) => new WpfVertex(dto);

            StartPathFindCommand = new RelayCommand(ExecuteStartPathFindCommand, CanExecuteStartFindPathCommand);
            CreateNewGraphCommand = new RelayCommand(ExecuteCreateNewGraphCommand, obj => true);
            ClearGraphCommand = new RelayCommand(ExecuteClearGraphCommand, obj=> Graph != null);
            SaveGraphCommand = new RelayCommand(ExecuteSaveGraphCommand, obj => Graph != null);
            LoadGraphCommand = new RelayCommand(ExecuteLoadGraphCommand, obj => true);
            ChangeVertexSize = new RelayCommand(ExecuteChangeVertexSize, obj => Graph != null);
        }

        public override void FindPath()
        {
            PrepareWindow(new PathFindingViewModel(this), new PathFindWindow());
        }

        public override void CreateNewGraph()
        {
            PrepareWindow(new GraphCreatingViewModel(this), new GraphCreatesWindow());
        }

        public void ExecuteChangeVertexSize(object param)
        {
            PrepareWindow(new VertexSizeChangingViewModel(this), new VertexSizeChangeWindow());
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
            return Graph.End != NullVertex.Instance
                && Graph.Start != NullVertex.Instance && Graph.Any()
                && !Graph.Start.IsVisited;
        }

        private void ExecuteLoadGraphCommand(object param)
        {
            base.LoadGraph();
            if (Graph != NullGraph.Instance)
            {
                OnPropertyChanged(nameof(GraphField));
                OnPropertyChanged(nameof(Graph));
                OnPropertyChanged(nameof(GraphParametres));
                WindowAdjust.Adjust(Graph);
            }
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

        protected override string GetSavingPath()
        {
            var save = new SaveFileDialog();
            return save.ShowDialog() == true ? save.FileName : string.Empty;
        }

        protected override string GetLoadingPath()
        {
            var open = new OpenFileDialog();
            return open.ShowDialog() == true ? open.FileName : string.Empty;
        }
    }
}
