using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfVersion.Infrastructure;
using WpfVersion.Model;
using WpfVersion.View.Windows;
using WpfVersion.Model.EventHolder;
using GraphLib.Vertex;
using GraphLib.GraphField;
using Microsoft.Win32;
using WpfVersion.Model.Vertex;
using System.Linq;
using GraphViewModel;
using GraphLib.Extensions;
using GraphViewModel.Interfaces;

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
        public override string PathFindingStatistics 
        {
            get { return statistics; }
            set { statistics = value; OnPropertyChanged(); }
        }

        private IGraphField graphField;
        public override IGraphField GraphField 
        { 
            get { return graphField; } 
            set 
            { 
                graphField = value; 
                OnPropertyChanged();
                WindowAdjust.Adjust(Graph);
            }
        }

        public RelayCommand StartPathFindCommand { get; }
        public RelayCommand CreateNewGraphCommand { get; }
        public RelayCommand ClearGraphCommand { get; }
        public RelayCommand SaveGraphCommand { get; }
        public RelayCommand LoadGraphCommand { get; }
        public RelayCommand ChangeVertexSize { get; }
        public RelayCommand ShowVertexCost { get; }

        public MainWindowViewModel()
        {
            GraphField = new WpfGraphField();
            VertexEventHolder = new WpfVertexEventHolder();
            FieldFactory = new WpfGraphFieldFactory();
            DtoConverter = (dto) => new WpfVertex(dto);

            StartPathFindCommand = new RelayCommand(ExecuteStartPathFindCommand, CanExecuteStartFindPathCommand);
            CreateNewGraphCommand = new RelayCommand(ExecuteCreateNewGraphCommand, AlwaysExecutable);
            ClearGraphCommand = new RelayCommand(ExecuteClearGraphCommand, CanExecuteGraphOperation);
            SaveGraphCommand = new RelayCommand(ExecuteSaveGraphCommand, CanExecuteGraphOperation);
            LoadGraphCommand = new RelayCommand(ExecuteLoadGraphCommand, AlwaysExecutable);
            ChangeVertexSize = new RelayCommand(ExecuteChangeVertexSize, CanExecuteGraphOperation);
            ShowVertexCost = new RelayCommand(ExecuteShowVertexCostCommand, AlwaysExecutable);
        }

        private bool AlwaysExecutable(object param)
        {
            return true;
        }

        private bool CanExecuteGraphOperation(object param)
        {
            return !Graph.IsDefault;
        }

        public void ExecuteShowVertexCostCommand(object parametre)
        {
            if ((bool)parametre)
            {
                Graph.ToWeighted();
            }
            else
            {
                Graph.ToUnweighted();
            }
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

        private void ExecuteSaveGraphCommand(object param) => base.SaveGraph();

        private bool CanExecuteStartFindPathCommand(object param)
        {
            return !Graph.End.IsDefault
                && !Graph.Start.IsDefault
                && Graph.Any() 
                && !Graph.Start.IsVisited;
        }

        private void ExecuteLoadGraphCommand(object param)
        {
            base.LoadGraph();
        }

        private void ExecuteClearGraphCommand(object param)
        {
            base.ClearGraph();
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

        private string GetPath(FileDialog dialog)
        {
            return dialog.ShowDialog() == true 
                ? dialog.FileName 
                : string.Empty;
        }

        protected override string GetSavingPath()
        {
            return GetPath(new SaveFileDialog());
        }

        protected override string GetLoadingPath()
        {
            return GetPath(new OpenFileDialog());
        }
    }
}
