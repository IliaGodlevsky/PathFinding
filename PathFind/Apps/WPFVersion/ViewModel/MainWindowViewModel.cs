using Algorithm.Realizations;
using Common;
using Common.Extensions;
using Common.Interfaces;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interface;
using GraphLib.Serialization.Interfaces;
using GraphViewModel;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WPFVersion.Infrastructure;
using WPFVersion.Model;
using WPFVersion.View.Windows;

namespace WPFVersion.ViewModel
{
    internal class MainWindowViewModel : MainModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
                WindowService.Adjust(Graph);
            }
        }

        public ICommand StartPathFindCommand { get; }
        public ICommand CreateNewGraphCommand { get; }
        public ICommand ClearGraphCommand { get; }
        public ICommand SaveGraphCommand { get; }
        public ICommand LoadGraphCommand { get; }
        public ICommand ChangeVertexSize { get; }
        public ICommand ShowVertexCost { get; }

        public MainWindowViewModel(
            BaseGraphFieldFactory   fieldFactory,
            IVertexEventHolder      eventHolder,
            IGraphSerializer        graphSerializer,
            IGraphAssembler            graphFactory,
            IPathInput              pathInput) : base(fieldFactory, eventHolder, graphSerializer, graphFactory, pathInput)
        {
            graphSerializer.OnExceptionCaught += OnExceptionCaught;
            graphFactory.OnExceptionCaught += OnExceptionCaught;

            StartPathFindCommand    = new RelayCommand(ExecuteStartPathFindCommand, CanExecuteStartFindPathCommand);
            CreateNewGraphCommand   = new RelayCommand(ExecuteCreateNewGraphCommand);
            ClearGraphCommand       = new RelayCommand(ExecuteClearGraphCommand, CanExecuteGraphOperation);
            SaveGraphCommand        = new RelayCommand(ExecuteSaveGraphCommand, CanExecuteGraphOperation);
            LoadGraphCommand        = new RelayCommand(ExecuteLoadGraphCommand);
            ChangeVertexSize        = new RelayCommand(ExecuteChangeVertexSize, CanExecuteGraphOperation);
            ShowVertexCost          = new RelayCommand(ExecuteShowVertexCostCommand);
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
            try
            {
                AlgorithmsPluginLoader.LoadAlgorithms(GetAlgorithmsLoadPath());
                var viewModel = new PathFindingViewModel(this)
                {
                    EndPoints = EndPoints
                };
                viewModel.OnPathNotFound += OnPathNotFound;
                PrepareWindow(viewModel, new PathFindWindow());
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
            }
        }

        public override void CreateNewGraph()
        {
            try
            {                
                var model = new GraphCreatingViewModel(this, graphAssembler);
                var window = new GraphCreatesWindow();
                PrepareWindow(model, window);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
            }
        }

        public void ExecuteChangeVertexSize(object param)
        {
            PrepareWindow(new VertexSizeChangingViewModel(this), new VertexSizeChangeWindow());
        }

        protected override string GetAlgorithmsLoadPath()
        {
            return ConfigurationManager.AppSettings["pluginsPath"];
        }

        private void PrepareWindow(IViewModel model, Window window)
        {
            window.DataContext = model;
            model.OnWindowClosed += (sender, args) => window.Close();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
        }

        private void ExecuteSaveGraphCommand(object param)
        {
            base.SaveGraph();
        }

        private bool CanExecuteStartFindPathCommand(object param)
        {
            return EndPoints.HasEndPointsSet;
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

        private bool CanExecuteGraphOperation(object param)
        {
            return !Graph.IsDefault();
        }

        private void OnPathNotFound(string message)
        {
            MessageBox.Show(message);
        }

        private void OnExceptionCaught(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }


    }
}
