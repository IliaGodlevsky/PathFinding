using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EndPoints;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Serialization;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Logging.Interface;
using NullObject.Extensions;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WPFVersion.DependencyInjection;
using WPFVersion.Enums;
using WPFVersion.Extensions;
using WPFVersion.Infrastructure;
using WPFVersion.Messages;
using WPFVersion.Model;
using WPFVersion.Model.VerticesCostMode;
using WPFVersion.View.Windows;

namespace WPFVersion.ViewModel
{
    public class MainWindowViewModel : MainModel, IMainModel, INotifyPropertyChanged, IDisposable
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

        public MainWindowViewModel(IGraphFieldFactory fieldFactory, IVertexEventHolder eventHolder,
            GraphSerializationModule SerializationModule, BaseEndPoints endPoints, ILog log)
            : base(fieldFactory, eventHolder, SerializationModule, endPoints, log)
        {
            messenger = DI.Container.Resolve<IMessenger>();
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
            messenger.Register<IsAllAlgorithmsFinishedMessage>(this, MessageTokens.MainModel, OnIsAllAlgorithmsFinished);
            messenger.Register<GraphCreatedMessage>(this, MessageTokens.MainModel, SetGraph);
        }

        public override void FindPath() => DI.Container.Resolve<PathFindWindow>().Show();

        public override void CreateNewGraph() => DI.Container.Resolve<GraphCreatesWindow>().Show();

        public override void ConnectNewGraph(IGraph graph)
        {
            costColors = new CostColors(graph);
            base.ConnectNewGraph(graph);
            WindowService.Adjust(graph);
            messenger
                .Forward(new ClearStatisticsMessage(), MessageTokens.AlgorithmStatisticsModel)
                .Forward(new GraphCreatedMessage(graph), MessageTokens.AlgorithmStatisticsModel);
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
        {
            messenger.Forward(new InterruptAllAlgorithmsMessage(), MessageTokens.AlgorithmStatisticsModel);
        }
        private void ExecuteClearGraphCommand(object param)
        {
            base.ClearGraph();
            messenger.Forward(new ClearStatisticsMessage(), MessageTokens.AlgorithmStatisticsModel);
        }

        private bool CanExecuteGraphOperation(object param) => !Graph.IsNull();
        private bool CanExecuteOperation(object param) => IsAllAlgorithmsFinished;
        private bool CanExecuteInterruptAlgorithmCommand(object param) => !IsAllAlgorithmsFinished;
        private bool CanExecuteClearGraphOperation(object param) => CanExecuteOperation(param) && CanExecuteGraphOperation(param);
        private bool CanExecuteColorizingGraphOperation(object param) => CanExecuteGraphOperation(param) && IsAllAlgorithmsFinished;

        private void SetGraph(GraphCreatedMessage message) => ConnectNewGraph(message.Graph);
        private void OnIsAllAlgorithmsFinished(IsAllAlgorithmsFinishedMessage message)
        {
            IsAllAlgorithmsFinished = message.IsAllAlgorithmsFinished;
        }

        public void Dispose()
        {
            messenger.Unregister(this);
        }

        private CostColors costColors;

        private readonly IMessenger messenger;
    }
}