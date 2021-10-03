using Common.Extensions;
using Common.Interface;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Logging.Interface;
using NullObject.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WPFVersion.Enums;
using WPFVersion.Extensions;
using WPFVersion.Infrastructure;
using WPFVersion.Messages;
using WPFVersion.Model;
using WPFVersion.Model.VerticesCostMode;
using WPFVersion.View.Windows;

namespace WPFVersion.ViewModel
{
    internal class MainWindowViewModel : MainModel, IMainModel, IModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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

        public AlgorithmViewModel SelectedAlgorithm { get; set; }

        private ObservableCollection<AlgorithmViewModel> statistics;
        public ObservableCollection<AlgorithmViewModel> Statistics
        {
            get => statistics ?? (statistics = new ObservableCollection<AlgorithmViewModel>());
            set { statistics = value; OnPropertyChanged(); }
        }

        private IGraphField graphField;
        public override IGraphField GraphField
        {
            get => graphField;
            set { graphField = value; OnPropertyChanged(); WindowService.Adjust(Graph); }
        }

        private int StartedAlgorithmsCount { get; set; }
        private int FinishedAlgorithmsCount { get; set; }

        public ICommand InterruptSelelctedAlgorithm { get; }
        public ICommand StartPathFindCommand { get; }
        public ICommand CreateNewGraphCommand { get; }
        public ICommand ClearGraphCommand { get; }
        public ICommand SaveGraphCommand { get; }
        public ICommand LoadGraphCommand { get; }
        public ICommand ShowVertexCost { get; }
        public ICommand InterruptAlgorithmCommand { get; }
        public ICommand ClearVerticesColor { get; }

        public MainWindowViewModel(IGraphFieldFactory fieldFactory, IVertexEventHolder eventHolder,
            ISaveLoadGraph saveLoad, IEnumerable<IGraphAssemble> graphAssembles, BaseEndPoints endPoints, ILog log)
            : base(fieldFactory, eventHolder, saveLoad, graphAssembles, endPoints, log)
        {
            ClearVerticesColor = new RelayCommand(ExecuteClearVerticesColors, CanExecuteClearGraphOperation);
            StartPathFindCommand = new RelayCommand(ExecuteStartPathFindCommand, CanExecuteStartFindPathCommand);
            CreateNewGraphCommand = new RelayCommand(ExecuteCreateNewGraphCommand, CanExecuteOperation);
            ClearGraphCommand = new RelayCommand(ExecuteClearGraphCommand, CanExecuteClearGraphOperation);
            SaveGraphCommand = new RelayCommand(ExecuteSaveGraphCommand, CanExecuteGraphOperation);
            LoadGraphCommand = new RelayCommand(ExecuteLoadGraphCommand, CanExecuteOperation);
            ShowVertexCost = new RelayCommand(ExecuteShowVertexCostCommand, CanExecuteOperation);
            InterruptAlgorithmCommand = new RelayCommand(ExecuteInterruptAlgorithmCommand, CanExecuteInterruptAlgorithmCommand);
            InterruptSelelctedAlgorithm = new RelayCommand(ExecuteInterruptCurrentAlgorithmCommand, CanExecuteInterruptCurrentAlgorithmCommand);
            Messenger.Default.Register<UpdateAlgorithmStatisticsMessage>(this, Constants.MessageToken, UpdateAlgorithmStatistics);
            Messenger.Default.Register<AlgorithmStartedMessage>(this, Constants.MessageToken, OnAlgorithmStarted);
            Messenger.Default.Register<AlgorithmFinishedMessage>(this, Constants.MessageToken, OnAlgorithmFinished);
        }

        public void ExecuteShowVertexCostCommand(object parametre)
        {
            (parametre as IVerticesCostsMode)?.Apply(Graph);
        }

        public override void FindPath()
        {
            try
            {
                var viewModel = new PathFindingViewModel(log, this, endPoints);
                var window = new PathFindWindow();
                PrepareWindow(viewModel, window);
            }
            catch (SystemException ex)
            {
                log.Warn(ex);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public override void CreateNewGraph()
        {
            try
            {
                var model = new GraphCreatingViewModel(log, this, graphAssembles);
                var window = new GraphCreatesWindow();
                PrepareWindow(model, window);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public override void ConnectNewGraph(IGraph graph)
        {
            base.ConnectNewGraph(graph);
            Statistics.Clear();
            FinishedAlgorithmsCount = 0;
            StartedAlgorithmsCount = 0;
        }

        private void PrepareWindow(IViewModel model, Window window)
        {
            window.DataContext = model;
            model.WindowClosed += (sender, args) => window.Close();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
        }

        private void ExecuteInterruptAlgorithmCommand(object param)
        {
            Statistics.ForEach(stat => stat.TryInterrupt());
        }

        private void ExecuteClearVerticesColors(object param)
        {
            ClearColors();
        }

        private void ExecuteInterruptCurrentAlgorithmCommand(object param)
        {
            SelectedAlgorithm.TryInterrupt();
        }

        private bool CanExecuteInterruptCurrentAlgorithmCommand(object param)
        {
            return SelectedAlgorithm != null;
        }

        private bool CanExecuteInterruptAlgorithmCommand(object sender)
        {
            return StartedAlgorithmsCount != FinishedAlgorithmsCount;
        }

        private void ExecuteSaveGraphCommand(object param) => base.SaveGraph();

        private bool CanExecuteStartFindPathCommand(object param) => !endPoints.HasIsolators();

        private void ExecuteLoadGraphCommand(object param) => base.LoadGraph();

        private void ExecuteClearGraphCommand(object param)
        {
            base.ClearGraph();
            Statistics.Clear();
            StartedAlgorithmsCount = 0;
            FinishedAlgorithmsCount = 0;
        }

        private void OnAlgorithmFinished(AlgorithmFinishedMessage message)
        {
            FinishedAlgorithmsCount++;
            Statistics[message.Index].Status = AlgorithmStatus.Finished;
        }

        private void OnAlgorithmStarted(AlgorithmStartedMessage message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Statistics.Add(new AlgorithmViewModel(message.Algorithm, message.AlgorithmName));
                var msg = new AlgorithmStatisticsIndexMessage(StartedAlgorithmsCount++);
                Messenger.Default.Send(msg, Constants.MessageToken);
            });
        }

        private void UpdateAlgorithmStatistics(UpdateAlgorithmStatisticsMessage message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Statistics[message.Index].Time = message.Time;
                Statistics[message.Index].Status = message.Status;
                Statistics[message.Index].PathCost = message.PathCost;
                Statistics[message.Index].PathLength = message.PathLength;
                Statistics[message.Index].VisitedVerticesCount = message.VisitedVertices;
            });
        }

        private void ExecuteStartPathFindCommand(object param) => FindPath();

        private void ExecuteCreateNewGraphCommand(object param) => CreateNewGraph();

        private bool CanExecuteGraphOperation(object param) => !Graph.IsNull();

        private bool CanExecuteOperation(object param)
        {
            return StartedAlgorithmsCount == FinishedAlgorithmsCount;
        }

        private bool CanExecuteClearGraphOperation(object param)
        {
            return CanExecuteOperation(param) && CanExecuteGraphOperation(param);
        }
    }
}