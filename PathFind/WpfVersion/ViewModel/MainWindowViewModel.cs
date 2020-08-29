using GraphLibrary.Common.Constants;
using GraphLibrary.Graph;
using GraphLibrary.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfVersion.Infrastructure;
using WpfVersion.Model;
using WpfVersion.Model.GraphLoader;
using WpfVersion.Model.GraphSaver;
using WpfVersion.Resources;
using WpfVersion.View.Windows;

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

        private AbstractGraph graph;
        public override AbstractGraph Graph 
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
            Format = ViewModelResources.GraphParametresFormat;
            saver = new WpfGraphSaver();
            loader = new WpfGraphLoader(VertexSize.SIZE_BETWEEN_VERTICES);
            filler = new WpfGraphFiller();
            StartPathFindCommand = new RelayCommand(ExecuteStartPathFindCommand, 
                obj => Graph?.End != null && Graph?.Start != null);
            CreateNewGraphCommand = new RelayCommand(ExecuteCreateNewGraphCommand, obj => true);
            ClearGraphCommand = new RelayCommand(ExecuteClearGraphCommand, obj=> Graph != null);
            SaveGraphCommand = new RelayCommand(ExecuteSaveGraphCommand, obj => Graph != null);
            LoadGraphCommand = new RelayCommand(ExecuteLoadGraphCommand, obj => true);
        }

        public override void PathFind()
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
            PathFind();
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
    }
}
