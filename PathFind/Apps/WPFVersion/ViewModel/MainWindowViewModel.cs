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
        public override string GraphParametres
        {
            get => graphParametres;
            set { graphParametres = value; OnPropertyChanged(); }
        }

        private IGraphField graphField;
        public override IGraphField GraphField
        {
            get => graphField;
            set { graphField = value; OnPropertyChanged(); WindowService.Adjust(Graph); }
        }

        private bool IsAllAlgorithmsFinished { get; set; } = true;

        public ICommand StartPathFindCommand { get; }
        public ICommand CreateNewGraphCommand { get; }
        public ICommand ClearGraphCommand { get; }
        public ICommand SaveGraphCommand { get; }
        public ICommand LoadGraphCommand { get; }
        public ICommand ShowVertexCost { get; }
        public ICommand InterruptAlgorithmCommand { get; }
        public ICommand ClearVerticesColorCommand { get; }

        public MainWindowViewModel(IGraphFieldFactory fieldFactory, IVertexEventHolder eventHolder,
            ISaveLoadGraph saveLoad, IEnumerable<IGraphAssemble> graphAssembles, BaseEndPoints endPoints, ILog log)
            : base(fieldFactory, eventHolder, saveLoad, graphAssembles, endPoints, log)
        {
            ClearVerticesColorCommand = new RelayCommand(ExecuteClearVerticesColors, CanExecuteClearGraphOperation);
            StartPathFindCommand = new RelayCommand(ExecuteStartPathFindCommand, CanExecuteStartFindPathCommand);
            CreateNewGraphCommand = new RelayCommand(ExecuteCreateNewGraphCommand, CanExecuteOperation);
            ClearGraphCommand = new RelayCommand(ExecuteClearGraphCommand, CanExecuteClearGraphOperation);
            SaveGraphCommand = new RelayCommand(ExecuteSaveGraphCommand, CanExecuteGraphOperation);
            LoadGraphCommand = new RelayCommand(ExecuteLoadGraphCommand, CanExecuteOperation);
            ShowVertexCost = new RelayCommand(ExecuteShowVertexCostCommand, CanExecuteOperation);
            InterruptAlgorithmCommand = new RelayCommand(ExecuteInterruptAlgorithmCommand, CanExecuteInterruptAlgorithmCommand);
            Messenger.Default.Register<AlgorithmsFinishedStatusMessage>(this, MessageTokens.MainModel, OnIsAllAlgorithmsFinished);
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
            Messenger.Default.Send(new ClearStatisticsMessage(), MessageTokens.AlgorithmStatisticsModel);
        }

        private void PrepareWindow(IViewModel model, Window window)
        {
            window.DataContext = model;
            model.WindowClosed += (sender, args) => window.Close();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
        }

        private void OnIsAllAlgorithmsFinished(AlgorithmsFinishedStatusMessage message)
        {
            IsAllAlgorithmsFinished = message.IsAllAlgorithmsFinished;
        }

        private void ExecuteInterruptAlgorithmCommand(object param)
        {
            Messenger.Default.Send(new InterruptAllAlgorithmsMessage(), MessageTokens.AlgorithmStatisticsModel);
        }

        private void ExecuteClearVerticesColors(object param)
        {
            ClearColors();
        }

        private void ExecuteSaveGraphCommand(object param) => base.SaveGraph();

        private bool CanExecuteStartFindPathCommand(object param) => !endPoints.HasIsolators();

        private void ExecuteLoadGraphCommand(object param) => base.LoadGraph();

        private void ExecuteClearGraphCommand(object param)
        {
            base.ClearGraph();
            Messenger.Default.Send(new ClearStatisticsMessage(), MessageTokens.AlgorithmStatisticsModel);
        }

        private void ExecuteStartPathFindCommand(object param) => FindPath();

        private void ExecuteCreateNewGraphCommand(object param) => CreateNewGraph();

        private bool CanExecuteGraphOperation(object param) => !Graph.IsNull();

        private bool CanExecuteOperation(object param)
        {
            return IsAllAlgorithmsFinished;
        }

        private bool CanExecuteInterruptAlgorithmCommand(object param)
        {
            return !IsAllAlgorithmsFinished;
        }

        private bool CanExecuteClearGraphOperation(object param)
        {
            return CanExecuteOperation(param) && CanExecuteGraphOperation(param);
        }
    }
}