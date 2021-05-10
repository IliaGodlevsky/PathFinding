using AssembleClassesLib.Interface;
using AssembleClassesLib.Realizations;
using AssembleClassesLib.Realizations.AssembleClassesImpl;
using Common.Extensions;
using Common.Interface;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization.Interfaces;
using GraphViewModel;
using Logging;
using System;
using System.ComponentModel;
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
            IGraphAssemble graphFactory,
            IPathInput pathInput,
            IAssembleClasses assembleClasses,
            Logs log)
            : base(fieldFactory, eventHolder, graphSerializer,
                  graphFactory, pathInput, assembleClasses, log)
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
            if (parametre is bool mustDisplayCost)
            {
                if (mustDisplayCost)
                {
                    Graph.ToWeighted();
                }
                else
                {
                    Graph.ToUnweighted();
                }
            }
        }

        public override void FindPath()
        {
            try
            {
                var notifingAssembleClasses = new NotifingAssembleClasses((AssembleClasses)assembleClasses);
                var updatableAssembleClasses = new UpdatableAssembleClasses(notifingAssembleClasses);
                void Interrupt(object sender, EventArgs e) => updatableAssembleClasses.Interrupt();
                var viewModel = new PathFindingViewModel(log, updatableAssembleClasses, this, EndPoints);
                var window = new PathFindWindow();
                notifingAssembleClasses.OnClassesLoaded += viewModel.UpdateAlgorithmKeys;
                updatableAssembleClasses.OnExceptionCaught += log.Warn;
                updatableAssembleClasses.LoadClasses();
                window.Closing += Interrupt;
                viewModel.OnEventHappened += OnExternalEventHappened;
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
                var model = new GraphCreatingViewModel(log, this, graphAssembler);
                var window = new GraphCreatesWindow();
                model.OnEventHappened += OnExternalEventHappened;
                PrepareWindow(model, window);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public void ExecuteChangeVertexSize(object param)
        {
            var model = new VertexSizeChangingViewModel(this, fieldFactory);
            var window = new VertexSizeChangeWindow();
            PrepareWindow(model, window);
        }

        protected override void OnExternalEventHappened(string message)
        {
            MessageBox.Show(message);
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
            return !Graph.IsNullObject();
        }
    }
}
