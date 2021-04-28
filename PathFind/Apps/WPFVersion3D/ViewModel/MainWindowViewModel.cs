using Algorithm.Realizations;
using AssembleClassesLib.Realizations;
using Common.Extensions;
using Common.Interface;
using Common.Logging;
using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization.Interfaces;
using GraphViewModel;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WPFVersion3D.Enums;
using WPFVersion3D.Infrastructure;
using WPFVersion3D.Infrastructure.Animators.Interface;
using WPFVersion3D.Model;
using WPFVersion3D.View;

namespace WPFVersion3D.ViewModel
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
            set { graphField = value; OnPropertyChanged(); }
        }

        public ICommand StartPathFindCommand { get; }
        public ICommand CreateNewGraphCommand { get; }
        public ICommand ClearGraphCommand { get; }
        public ICommand SaveGraphCommand { get; }
        public ICommand LoadGraphCommand { get; }
        public ICommand ChangeOpacityCommand { get; }
        public ICommand AnimatedAxisRotateCommand { get; }

        public MainWindowViewModel(BaseGraphFieldFactory fieldFactory,
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
            ChangeOpacityCommand = new RelayCommand(ExecuteChangeOpacity, CanExecuteGraphOperation);
            AnimatedAxisRotateCommand = new RelayCommand(ExecuteAnimatedAxisRotateCommand);
        }

        public override void FindPath()
        {
            try
            {
                string loadPath = GetAlgorithmsLoadPath();
                var algorithmClasses = new ConcreteAssembleAlgorithmClasses(loadPath);
                algorithmClasses.LoadClasses();
                var notifyableAssembleClasses = new NotifingAssembleClasses(algorithmClasses);
                var updatableAssembleClasses = new UpdatableAssembleClasses(notifyableAssembleClasses);
                var viewModel = new PathFindingViewModel(updatableAssembleClasses, this, EndPoints);
                var window = new PathFindWindow();
                notifyableAssembleClasses.OnClassesLoaded += viewModel.UpdateAlgorithmKeys;
                updatableAssembleClasses.OnExceptionCaught += (ex, msg) =>
                {
                    OnExceptionCaught(ex, msg);
                    Logger.Instance.Warn(ex);
                };
                updatableAssembleClasses.LoadClasses();
                window.Closing += (s, e) => updatableAssembleClasses.Interrupt();
                viewModel.OnEventHappened += OnExternalEventHappened;
                PrepareWindow(viewModel, window);
            }
            catch (SystemException ex)
            {
                OnExceptionCaught(ex);
                Logger.Instance.Warn(ex);
            }
            catch (Exception ex)
            {
                OnExceptionCaught(ex);
                Logger.Instance.Error(ex);
            }
        }

        public override void CreateNewGraph()
        {
            try
            {
                var model = new GraphCreatingViewModel(this, graphAssembler);
                var window = new GraphCreateWindow();
                model.OnEventHappened += OnExternalEventHappened;
                PrepareWindow(model, window);
            }
            catch (Exception ex)
            {
                OnExceptionCaught(ex);
                Logger.Instance.Error(ex);
            }
        }

        public void StretchAlongXAxis(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            (GraphField as GraphField3D)?.StretchAlongAxis(Axis.Abscissa, e.NewValue, 1, 0, 0);
        }

        public void StretchAlongYAxis(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            (GraphField as GraphField3D)?.StretchAlongAxis(Axis.Ordinate, e.NewValue, 0, 1, 0);
        }

        public void StretchAlongZAxis(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            (GraphField as GraphField3D)?.StretchAlongAxis(Axis.Applicate, e.NewValue, 0, 0, 1);
        }

        private void ChangeVerticesOpacity()
        {
            var model = new OpacityChangeViewModel();
            var window = new OpacityChangeWindow();
            PrepareWindow(model, window);
        }

        private void ExecuteSaveGraphCommand(object param)
        {
            base.SaveGraph();
        }

        private void ExecuteChangeOpacity(object param)
        {
            ChangeVerticesOpacity();
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

        private void ExecuteAnimatedAxisRotateCommand(object param)
        {
            var rotator = param as IAnimator;
            rotator?.ApplyAnimation();
        }

        private void PrepareWindow(IViewModel model, Window window)
        {
            model.OnWindowClosed += (sender, args) => window.Close();
            window.DataContext = model;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
        }

        protected override void OnExternalEventHappened(string message)
        {
            MessageBox.Show(message);
        }

        protected override void OnExceptionCaught(Exception ex, string additaionalMessage = "")
        {
            MessageBox.Show(ex.Message, additaionalMessage);
        }

        private bool CanExecuteGraphOperation(object param)
        {
            return !Graph.IsNullObject();
        }

        protected override string GetAlgorithmsLoadPath()
        {
            return ConfigurationManager.AppSettings["pluginsPath"];
        }
    }
}
