using AssembleClassesLib.Extensions;
using AssembleClassesLib.Interface;
using AssembleClassesLib.Realizations;
using AssembleClassesLib.Realizations.AssembleClassesImpl;
using Common.Interface;
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
using WPFVersion3D.Axes;
using WPFVersion3D.Infrastructure;
using WPFVersion3D.Interface;
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

        public IDictionary<string, IAnimationSpeed> AnimationSpeeds => animationSpeeds.Value;

        public ICommand StartPathFindCommand { get; }
        public ICommand CreateNewGraphCommand { get; }
        public ICommand ClearGraphCommand { get; }
        public ICommand SaveGraphCommand { get; }
        public ICommand LoadGraphCommand { get; }
        public ICommand ChangeOpacityCommand { get; }
        public ICommand AnimatedAxisRotateCommand { get; }

        public MainWindowViewModel(
            IGraphFieldFactory fieldFactory,
            IVertexEventHolder eventHolder,
            ISaveLoadGraph saveLoad,
            IEnumerable<IGraphAssemble> graphAssembles,
            IAssembleClasses assembleClasses,
            ILog log)
            : base(fieldFactory, eventHolder, saveLoad,
                  graphAssembles, assembleClasses, log)
        {
            StartPathFindCommand = new RelayCommand(ExecuteStartPathFindCommand, CanExecuteStartFindPathCommand);
            CreateNewGraphCommand = new RelayCommand(ExecuteCreateNewGraphCommand);
            ClearGraphCommand = new RelayCommand(ExecuteClearGraphCommand, CanExecuteGraphOperation);
            SaveGraphCommand = new RelayCommand(ExecuteSaveGraphCommand, CanExecuteGraphOperation);
            LoadGraphCommand = new RelayCommand(ExecuteLoadGraphCommand);
            ChangeOpacityCommand = new RelayCommand(ExecuteChangeOpacity, CanExecuteGraphOperation);
            AnimatedAxisRotateCommand = new RelayCommand(ExecuteAnimatedAxisRotateCommand);
            animationSpeeds = new Lazy<IDictionary<string, IAnimationSpeed>>(GetAnimationSpeeds);
        }

        public override void FindPath()
        {
            try
            {
                var notifyableAssembleClasses = new NotifingAssembleClasses((AssembleClasses)algorithmClasses);
                var updatableAssembleClasses = new UpdatableAssembleClasses(notifyableAssembleClasses);
                void Interrupt(object sender, EventArgs e) => updatableAssembleClasses.Interrupt();
                var viewModel = new PathFindingViewModel(log, updatableAssembleClasses, this, EndPoints);
                var window = new PathFindWindow();
                notifyableAssembleClasses.OnClassesLoaded += viewModel.UpdateAlgorithmKeys;
                updatableAssembleClasses.OnExceptionCaught += log.Warn;
                updatableAssembleClasses.LoadClasses();
                window.Closing += Interrupt;
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
                var window = new GraphCreateWindow();
                PrepareWindow(model, window);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public void StretchAlongXAxis(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            (GraphField as GraphField3D)?.StretchAlongAxis(new Abscissa(), e.NewValue, 1, 0, 0);
        }

        public void StretchAlongYAxis(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            (GraphField as GraphField3D)?.StretchAlongAxis(new Ordinate(), e.NewValue, 0, 1, 0);
        }

        public void StretchAlongZAxis(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            (GraphField as GraphField3D)?.StretchAlongAxis(new Applicate(), e.NewValue, 0, 0, 1);
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
            (graphField as GraphField3D)?.CenterGraph();
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
            var rotator = param as IAnimatedAxisRotator;
            rotator?.RotateAxis();
        }

        private void PrepareWindow(IViewModel model, Window window)
        {
            model.OnWindowClosed += (sender, args) => window.Close();
            window.DataContext = model;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
        }

        private bool CanExecuteGraphOperation(object param)
        {
            return !Graph.IsNullObject();
        }

        private IDictionary<string, IAnimationSpeed> GetAnimationSpeeds()
        {
            var speeds = new AnimationSpeedClasses();
            speeds.LoadClasses();
            return speeds.AsNameInstanceDictionary<IAnimationSpeed>();
        }

        private readonly Lazy<IDictionary<string, IAnimationSpeed>> animationSpeeds;
    }
}
