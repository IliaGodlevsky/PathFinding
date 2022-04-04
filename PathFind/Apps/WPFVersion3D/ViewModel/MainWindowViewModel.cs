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
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Enums;
using WPFVersion3D.Extensions;
using WPFVersion3D.Infrastructure;
using WPFVersion3D.Messages;
using WPFVersion3D.View;

namespace WPFVersion3D.ViewModel
{
    internal class MainWindowViewModel : MainModel, IMainModel, INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IMessenger messenger;

        private IGraphField graphField;
        private string graphParametres;

        private bool IsAllAlgorithmsFinished { get; set; } = true;

        public override string GraphParametres
        {
            get => graphParametres;
            set { graphParametres = value; OnPropertyChanged(); }
        }

        public override IGraphField GraphField
        {
            get => graphField;
            set { graphField = value; OnPropertyChanged(); }
        }

        public ICommand StartPathFindCommand { get; }

        public ICommand CreateNewGraphCommand { get; }

        public ICommand ClearGraphCommand { get; }

        public ICommand SaveGraphCommand { get; }

        public ICommand LoadGraphCommand { get; }

        public ICommand ChangeOpacityCommand { get; }

        public ICommand InterruptAlgorithmCommand { get; }

        public ICommand ClearVerticesColorCommand { get; }

        public MainWindowViewModel(IGraphFieldFactory fieldFactory, IVertexEventHolder eventHolder,
            GraphSerializationModule serializationModule, BaseEndPoints endPoints, ILog log)
            : base(fieldFactory, eventHolder, serializationModule, endPoints, log)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            ClearVerticesColorCommand = new RelayCommand(ExecuteClearVerticesColors, CanExecuteClearGraphOperation);
            StartPathFindCommand = new RelayCommand(ExecuteStartPathFindCommand, CanExecuteStartFindPathCommand);
            CreateNewGraphCommand = new RelayCommand(ExecuteCreateNewGraphCommand, CanExecuteCommand);
            ClearGraphCommand = new RelayCommand(ExecuteClearGraphCommand, CanExecuteClearGraphOperation);
            SaveGraphCommand = new RelayCommand(ExecuteSaveGraphCommand, CanExecuteGraphRelatedCommand);
            LoadGraphCommand = new RelayCommand(ExecuteLoadGraphCommand, CanExecuteCommand);
            ChangeOpacityCommand = new RelayCommand(ExecuteChangeOpacity, CanExecuteGraphRelatedCommand);
            InterruptAlgorithmCommand = new RelayCommand(ExecuteInterruptAlgorithmCommand, CanExecuteInterruptAlgorithmCommand);
            messenger.Register<IsAllAlgorithmsFinishedMessage>(this, MessageTokens.MainModel, OnIsAllAlgorithmsFinished);
            messenger.Register<GraphCreatedMessage>(this, MessageTokens.MainModel, SetGraph);
        }

        public void Dispose()
        {
            messenger.Unregister(this);
        }

        public override void FindPath()
        {
            DI.Container.Resolve<PathFindWindow>().Show();
        }

        public override void CreateNewGraph()
        {
            DI.Container.Resolve<GraphCreateWindow>().Show();
        }

        private void ChangeVerticesOpacity()
        {
            DI.Container.Resolve<OpacityChangeWindow>().Show();
        }

        public override void ConnectNewGraph(IGraph graph)
        {
            base.ConnectNewGraph(graph);
            messenger
                .Forward(new ClearStatisticsMessage(), MessageTokens.AlgorithmStatisticsModel)
                .Forward(new GraphFieldCreatedMessage(GraphField), MessageTokens.StretchAlongAxisModel);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ExecuteClearVerticesColors(object param)
        {
            ClearColors();
        }

        private void ExecuteSaveGraphCommand(object param)
        {
            base.SaveGraph();
        }

        private void ExecuteChangeOpacity(object param)
        {
            ChangeVerticesOpacity();
        }

        private void ExecuteLoadGraphCommand(object param)
        {
            base.LoadGraph();
        }

        private void ExecuteStartPathFindCommand(object param)
        {
            FindPath();
        }

        private void ExecuteCreateNewGraphCommand(object param)
        {
            CreateNewGraph();
        }

        private void ExecuteClearGraphCommand(object param)
        {
            base.ClearGraph();
            var message = new ClearStatisticsMessage();
            messenger.Forward(message, MessageTokens.AlgorithmStatisticsModel);
        }

        private void ExecuteInterruptAlgorithmCommand(object param)
        {
            var message = new InterruptAllAlgorithmsMessage();
            messenger.Forward(message, MessageTokens.AlgorithmStatisticsModel);
        }

        private bool CanExecuteStartFindPathCommand(object param)
        {
            return !endPoints.HasIsolators();
        }

        private bool CanExecuteClearGraphOperation(object param)
        {
            return CanExecuteGraphRelatedCommand(param) && CanExecuteCommand(param);
        }

        private bool CanExecuteGraphRelatedCommand(object param)
        {
            return !Graph.IsNull();
        }

        private bool CanExecuteCommand(object param)
        {
            return IsAllAlgorithmsFinished;
        }
        private bool CanExecuteInterruptAlgorithmCommand(object sender)
        {
            return !IsAllAlgorithmsFinished;
        }

        private void OnIsAllAlgorithmsFinished(IsAllAlgorithmsFinishedMessage message)
        {
            IsAllAlgorithmsFinished = message.IsAllAlgorithmsFinished;
        }

        private void SetGraph(GraphCreatedMessage message)
        {
            ConnectNewGraph(message.Value);
        }
    }
}