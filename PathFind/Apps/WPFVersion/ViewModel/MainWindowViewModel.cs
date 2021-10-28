using Algorithm.Factory;
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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
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
        public override string GraphParametres { get => graphParametres; set { graphParametres = value; OnPropertyChanged(); } }

        private IGraphField graphField;
        public override IGraphField GraphField { get => graphField; set { graphField = value; OnPropertyChanged(); } }

        private bool IsAllAlgorithmsFinished { get; set; } = true;

        public ICommand StartPathFindCommand { get; }
        public ICommand CreateNewGraphCommand { get; }
        public ICommand ClearGraphCommand { get; }
        public ICommand SaveGraphCommand { get; }
        public ICommand LoadGraphCommand { get; }
        public ICommand ShowVertexCost { get; }
        public ICommand InterruptAlgorithmCommand { get; }
        public ICommand ClearVerticesColorCommand { get; }
        public ICommand ColorizeAccordingToCostCommand { get; }
        public ICommand ResetColorizingCommand { get; }

        public MainWindowViewModel(IGraphFieldFactory fieldFactory, IVertexEventHolder eventHolder, ISaveLoadGraph saveLoad,
            IEnumerable<IGraphAssemble> graphAssembles, BaseEndPoints endPoints, IEnumerable<IAlgorithmFactory> algorithmFactories, ILog log)
            : base(fieldFactory, eventHolder, saveLoad, graphAssembles, endPoints, algorithmFactories, log)
        {
            ResetColorizingCommand = new RelayCommand(ExecuteResetColorizing, CanExecuteColorizingGraphOperation);
            ColorizeAccordingToCostCommand = new RelayCommand(ExecuteColorizeAccordingToCost, CanExecuteColorizingGraphOperation);
            ClearVerticesColorCommand = new RelayCommand(ExecuteClearVerticesColors, CanExecuteClearGraphOperation);
            StartPathFindCommand = new RelayCommand(ExecuteStartPathFindCommand, CanExecuteStartFindPathCommand);
            CreateNewGraphCommand = new RelayCommand(ExecuteCreateNewGraphCommand, CanExecuteOperation);
            ClearGraphCommand = new RelayCommand(ExecuteClearGraphCommand, CanExecuteClearGraphOperation);
            SaveGraphCommand = new RelayCommand(ExecuteSaveGraphCommand, CanExecuteGraphOperation);
            LoadGraphCommand = new RelayCommand(ExecuteLoadGraphCommand, CanExecuteOperation);
            ShowVertexCost = new RelayCommand(ExecuteShowVertexCostCommand, CanExecuteOperation);
            InterruptAlgorithmCommand = new RelayCommand(ExecuteInterruptAlgorithmCommand, CanExecuteInterruptAlgorithmCommand);
            Messenger.Default.Register<IsAllAlgorithmsFinishedMessage>(this, MessageTokens.MainModel, OnIsAllAlgorithmsFinished);
            Messenger.Default.Register<GraphCreatedMessage>(this, MessageTokens.MainModel, SetGraph);
        }

        public override void FindPath()
        {
            try
            {
                var viewModel = new PathFindingViewModel(log, Graph,
                    endPoints, algorithmFactories);
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
                var model = new GraphCreatingViewModel(log, graphAssembles);
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
            costColors = new CostColors(graph);
            base.ConnectNewGraph(graph);
            WindowService.Adjust(graph);
            Messenger.Default.Send(new ClearStatisticsMessage(), MessageTokens.AlgorithmStatisticsModel);
        }

        private void PrepareWindow(IViewModel model, Window window)
        {
            window.DataContext = model;
            model.WindowClosed += (sender, args) => window.Close();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
        }

        private void ExecuteColorizeAccordingToCost(object param) => costColors.ColorizeAccordingToCost();
        private void ExecuteResetColorizing(object param) => costColors.ReturnPreviousColors();
        private void ExecuteShowVertexCostCommand(object parametre) => (parametre as IVerticesCostsMode)?.Apply(Graph);
        private void ExecuteClearVerticesColors(object param) => ClearColors();
        private void ExecuteSaveGraphCommand(object param) => base.SaveGraph();
        private bool CanExecuteStartFindPathCommand(object param) => !endPoints.HasIsolators();
        private void ExecuteLoadGraphCommand(object param) => base.LoadGraph();
        private void ExecuteStartPathFindCommand(object param) => FindPath();
        private void ExecuteCreateNewGraphCommand(object param) => CreateNewGraph();
        private void ExecuteInterruptAlgorithmCommand(object param) 
            => Messenger.Default.Send(new InterruptAllAlgorithmsMessage(), MessageTokens.AlgorithmStatisticsModel);
        private void ExecuteClearGraphCommand(object param)
        {
            base.ClearGraph();
            Messenger.Default.Send(new ClearStatisticsMessage(), MessageTokens.AlgorithmStatisticsModel);
        }

        private bool CanExecuteGraphOperation(object param) => !Graph.IsNull();
        private bool CanExecuteOperation(object param) => IsAllAlgorithmsFinished;
        private bool CanExecuteInterruptAlgorithmCommand(object param) => !IsAllAlgorithmsFinished;
        private bool CanExecuteClearGraphOperation(object param) => CanExecuteOperation(param) && CanExecuteGraphOperation(param);
        private bool CanExecuteColorizingGraphOperation(object param) => CanExecuteGraphOperation(param) && IsAllAlgorithmsFinished;

        private void SetGraph(GraphCreatedMessage message) => ConnectNewGraph(message.Graph);
        private void OnIsAllAlgorithmsFinished(IsAllAlgorithmsFinishedMessage message)
            => IsAllAlgorithmsFinished = message.IsAllAlgorithmsFinished;

        private CostColors costColors;
    }
}