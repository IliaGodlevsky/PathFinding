using Algorithm.Realizations;
using Common.Extensions;
using Common.Interface;
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
using Algorithm.Common.Exceptions;
using Common.Logging;
using GraphLib.Exceptions;
using WPFVersion.Infrastructure;
using WPFVersion.Model;
using WPFVersion.View.Windows;

namespace WPFVersion.ViewModel
{
    internal class MainWindowViewModel : MainModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler OnAlgorithmInterrupted;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string graphParametres;
        public override string GraphParametres
        {
            get => graphParametres;
            set { graphParametres = value; OnPropertyChanged(); }
        }

        private string statistics;
        public override string PathFindingStatistics
        {
            get => statistics;
            set { statistics = value; OnPropertyChanged(); }
        }

        private IGraphField graphField;
        public override IGraphField GraphField
        {
            get => graphField;
            set
            {
                graphField = value;
                OnPropertyChanged();
                WindowService.Adjust(Graph);
            }
        }

        public bool CanInterruptAlgorithm { private get; set; }

        public ICommand StartPathFindCommand { get; }
        public ICommand CreateNewGraphCommand { get; }
        public ICommand ClearGraphCommand { get; }
        public ICommand SaveGraphCommand { get; }
        public ICommand LoadGraphCommand { get; }
        public ICommand ChangeVertexSize { get; }
        public ICommand ShowVertexCost { get; }
        public ICommand InterruptAlgorithmCommand { get; }

        public MainWindowViewModel(
            BaseGraphFieldFactory fieldFactory,
            IVertexEventHolder eventHolder,
            IGraphSerializer graphSerializer,
            IGraphAssembler graphFactory,
            IPathInput pathInput) 
            : base(fieldFactory, eventHolder, graphSerializer, graphFactory, pathInput)
        {
            StartPathFindCommand = new RelayCommand(ExecuteStartPathFindCommand, CanExecuteStartFindPathCommand);
            CreateNewGraphCommand = new RelayCommand(ExecuteCreateNewGraphCommand);
            ClearGraphCommand = new RelayCommand(ExecuteClearGraphCommand, CanExecuteGraphOperation);
            SaveGraphCommand = new RelayCommand(ExecuteSaveGraphCommand, CanExecuteGraphOperation);
            LoadGraphCommand = new RelayCommand(ExecuteLoadGraphCommand);
            ChangeVertexSize = new RelayCommand(ExecuteChangeVertexSize, CanExecuteGraphOperation);
            ShowVertexCost = new RelayCommand(ExecuteShowVertexCostCommand);
            InterruptAlgorithmCommand = new RelayCommand(ExecuteInterruptAlgorithmCommand, CanExecuteInterruptAlgorithmCommand);
        }

        public void ExecuteInterruptAlgorithmCommand(object sender)
        {
            OnAlgorithmInterrupted?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecuteInterruptAlgorithmCommand(object sender)
        {
            return CanInterruptAlgorithm;
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
                string loadPath = GetAlgorithmsLoadPath();
                AlgorithmsFactory.LoadAlgorithms(loadPath);
                var viewModel = new PathFindingViewModel(this);
                var listener = new PluginsWatcher(viewModel);
                var window = new PathFindWindow();
                window.Closing += listener.StopWatching;
                viewModel.OnExceptionCaught += OnExceptionCaught;
                viewModel.EndPoints = EndPoints;
                listener.FolderPath = loadPath;
                listener.StartWatching();
                PrepareWindow(viewModel, window);
            }
            catch (NoAlgorithmsLoadedException ex)
            {
                OnNotFatalExceptionCaught(ex);
            }
            catch (Exception ex)
            {
                OnNotFatalExceptionCaught(ex);
            }
        }

        public override void CreateNewGraph()
        {
            try
            {
                var model = new GraphCreatingViewModel(this, graphAssembler);
                var window = new GraphCreatesWindow();
                model.OnExceptionCaught += OnExceptionCaught;
                PrepareWindow(model, window);
            }
            catch (Exception ex)
            {
                OnErrorExceptionCaught(ex);
            }
        }

        public void ExecuteChangeVertexSize(object param)
        {
            var model = new VertexSizeChangingViewModel(this, fieldFactory);
            var window = new VertexSizeChangeWindow();
            PrepareWindow(model, window);
        }

        protected override void OnExceptionCaught(string message)
        {
            MessageBox.Show(message);
        }

        protected override void OnExceptionCaught(Exception ex, string additaionalMessage = "")
        {
            MessageBox.Show(ex.Message, additaionalMessage);
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
    }
}
