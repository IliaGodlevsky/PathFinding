using Common.Extensions;
using Common.Interface;
using GraphLib.Base;
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
using WPFVersion3D.Attributes;
using WPFVersion3D.Axes;
using WPFVersion3D.Enums;
using WPFVersion3D.Infrastructure;
using WPFVersion3D.Interface;
using WPFVersion3D.Model;
using WPFVersion3D.View;

namespace WPFVersion3D.ViewModel
{
    internal class MainWindowViewModel : MainModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler AlgorithmInterrupted;

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

        public ObservableCollection<string> statistics;
        public ObservableCollection<string> Statistics
        {
            get => statistics ?? (statistics = new ObservableCollection<string>());
            set { statistics = value; OnPropertyChanged(); }
        }

        private IGraphField graphField;
        public override IGraphField GraphField
        {
            get => graphField;
            set { graphField = value; OnPropertyChanged(); }
        }

        public bool IsAlgorithmStarted { private get; set; }

        public IDictionary<string, BaseSpeed> AnimationSpeeds => animationSpeeds.Value;

        public ICommand StartPathFindCommand { get; }
        public ICommand CreateNewGraphCommand { get; }
        public ICommand ClearGraphCommand { get; }
        public ICommand SaveGraphCommand { get; }
        public ICommand LoadGraphCommand { get; }
        public ICommand ChangeOpacityCommand { get; }
        public ICommand AnimatedAxisRotateCommand { get; }
        public ICommand InterruptAlgorithmCommand { get; }

        public MainWindowViewModel(IGraphFieldFactory fieldFactory, IVertexEventHolder eventHolder,
            ISaveLoadGraph saveLoad, IEnumerable<IGraphAssemble> graphAssembles, BaseEndPoints endPoints, ILog log)
            : base(fieldFactory, eventHolder, saveLoad, graphAssembles, endPoints, log)
        {
            StartPathFindCommand = new RelayCommand(ExecuteStartPathFindCommand, CanExecuteStartFindPathCommand);
            CreateNewGraphCommand = new RelayCommand(ExecuteCreateNewGraphCommand, CanExecuteOperation);
            ClearGraphCommand = new RelayCommand(ExecuteClearGraphCommand, CanExecuteClearGraphOperation);
            SaveGraphCommand = new RelayCommand(ExecuteSaveGraphCommand, CanExecuteGraphOperation);
            LoadGraphCommand = new RelayCommand(ExecuteLoadGraphCommand, CanExecuteOperation);
            ChangeOpacityCommand = new RelayCommand(ExecuteChangeOpacity, CanExecuteGraphOperation);
            AnimatedAxisRotateCommand = new RelayCommand(ExecuteAnimatedAxisRotateCommand);
            InterruptAlgorithmCommand = new RelayCommand(ExecuteInterruptAlgorithmCommand, CanExecuteInterruptAlgorithmCommand);
            animationSpeeds = new Lazy<IDictionary<string, BaseSpeed>>(GetSpeedDictionary);
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
                var window = new GraphCreateWindow();
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
            (graphField as GraphField3D)?.CenterGraph();
        }

        private void StretchAlongAxis(IAxis axis, double distanceBetween, params double[] offset)
            => (GraphField as GraphField3D)?.StretchAlongAxis(axis, distanceBetween, offset);

        public void StretchAlongXAxis(object sender, RoutedPropertyChangedEventArgs<double> e)
            => StretchAlongAxis(new Abscissa(), e.NewValue, 1, 0, 0);

        public void StretchAlongYAxis(object sender, RoutedPropertyChangedEventArgs<double> e)
            => StretchAlongAxis(new Ordinate(), e.NewValue, 0, 1, 0);

        public void StretchAlongZAxis(object sender, RoutedPropertyChangedEventArgs<double> e)
            => StretchAlongAxis(new Applicate(), e.NewValue, 0, 0, 1);

        private void ChangeVerticesOpacity()
        {
            var model = new OpacityChangeViewModel();
            var window = new OpacityChangeWindow();
            PrepareWindow(model, window);
        }

        private void ExecuteSaveGraphCommand(object param) => base.SaveGraph();

        private void ExecuteChangeOpacity(object param) => ChangeVerticesOpacity();

        private bool CanExecuteStartFindPathCommand(object param) => endPoints.HasEndPointsSet;

        private void ExecuteLoadGraphCommand(object param) => base.LoadGraph();

        private void ExecuteClearGraphCommand(object param)
        {
            base.ClearGraph();
            Statistics.Clear();
        }

        private void ExecuteStartPathFindCommand(object param) => FindPath();

        private void ExecuteCreateNewGraphCommand(object param) => CreateNewGraph();

        private void ExecuteInterruptAlgorithmCommand(object sender)
            => AlgorithmInterrupted?.Invoke(this, EventArgs.Empty);

        private bool CanExecuteInterruptAlgorithmCommand(object sender) => IsAlgorithmStarted;

        private void ExecuteAnimatedAxisRotateCommand(object param)
        {
            var rotator = param as IAnimatedAxisRotator;
            rotator?.RotateAxis();
        }

        private void PrepareWindow(IViewModel model, Window window)
        {
            model.WindowClosed += (sender, args) => window.Close();
            window.DataContext = model;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
        }

        private bool CanExecuteClearGraphOperation(object param)
            => CanExecuteGraphOperation(param) && CanExecuteOperation(param);

        private bool CanExecuteGraphOperation(object param) => !Graph.IsNull();

        private bool CanExecuteOperation(object param) => !IsAlgorithmStarted;

        private IDictionary<string, BaseSpeed> GetSpeedDictionary()
        {
            string Description(AnimationSpeed speed) => ((Enum)speed).GetDescription();
            BaseSpeed Speed(AnimationSpeed speed) => speed.GetAttributeOrNull<BaseSpeed>();
            return EnumExtensions.ToDictionary<string, BaseSpeed, AnimationSpeed>(Description, Speed);
        }

        private readonly Lazy<IDictionary<string, BaseSpeed>> animationSpeeds;
    }
}