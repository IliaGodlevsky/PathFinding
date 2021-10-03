using Common.Extensions;
using Common.Interface;
using EnumerationValues.Extensions;
using EnumerationValues.Realizations;
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
using WPFVersion3D.Attributes;
using WPFVersion3D.Axes;
using WPFVersion3D.Enums;
using WPFVersion3D.Extensions;
using WPFVersion3D.Infrastructure;
using WPFVersion3D.Interface;
using WPFVersion3D.Messages;
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
            set { graphField = value; OnPropertyChanged(); }
        }

        private int StartedAlgorithmsCount { get; set; }
        private int FinishedAlgorithmsCount { get; set; }

        public Tuple<string, BaseAnimationSpeed>[] AnimationSpeeds => animationSpeeds.Value;

        public ICommand InterruptSelelctedAlgorithm { get; }
        public ICommand StartPathFindCommand { get; }
        public ICommand CreateNewGraphCommand { get; }
        public ICommand ClearGraphCommand { get; }
        public ICommand SaveGraphCommand { get; }
        public ICommand LoadGraphCommand { get; }
        public ICommand ChangeOpacityCommand { get; }
        public ICommand AnimatedAxisRotateCommand { get; }
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
            ChangeOpacityCommand = new RelayCommand(ExecuteChangeOpacity, CanExecuteGraphOperation);
            AnimatedAxisRotateCommand = new RelayCommand(ExecuteAnimatedAxisRotateCommand);
            InterruptAlgorithmCommand = new RelayCommand(ExecuteInterruptAlgorithmCommand, CanExecuteInterruptAlgorithmCommand);
            InterruptSelelctedAlgorithm = new RelayCommand(ExecuteInterruptCurrentAlgorithmCommand, CanExecuteInterruptCurrentAlgorithmCommand);
            animationSpeeds = new Lazy<Tuple<string, BaseAnimationSpeed>[]>(GetSpeedTupleCollection);
            Messenger.Default.Register<UpdateAlgorithmStatisticsMessage>(this, Constants.MessageToken, UpdateAlgorithmStatistics);
            Messenger.Default.Register<AlgorithmStartedMessage>(this, Constants.MessageToken, OnAlgorithmStarted);
            Messenger.Default.Register<AlgorithmFinishedMessage>(this, Constants.MessageToken, OnAlgorithmFinished);
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
            FinishedAlgorithmsCount = 0;
            StartedAlgorithmsCount = 0;
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

        private void OnAlgorithmStarted(AlgorithmStartedMessage message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Statistics.Add(new AlgorithmViewModel(message.Algorithm, message.AlgorithmName));
                var msg = new AlgorithmStatisticsIndexMessage(StartedAlgorithmsCount++);
                Messenger.Default.Send(msg, Constants.MessageToken);
            });
        }

        private void OnAlgorithmFinished(AlgorithmFinishedMessage message)
        {
            FinishedAlgorithmsCount++;
            Statistics[message.Index].Status = AlgorithmStatus.Finished;
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

        private void ExecuteClearVerticesColors(object param)
        {
            ClearColors();
        }

        private void ExecuteSaveGraphCommand(object param) => base.SaveGraph();

        private void ExecuteChangeOpacity(object param) => ChangeVerticesOpacity();

        private bool CanExecuteStartFindPathCommand(object param) => !endPoints.HasIsolators();

        private void ExecuteLoadGraphCommand(object param) => base.LoadGraph();

        private void ExecuteClearGraphCommand(object param)
        {
            base.ClearGraph();
            Statistics.Clear();
        }

        private void ExecuteStartPathFindCommand(object param) => FindPath();

        private void ExecuteCreateNewGraphCommand(object param) => CreateNewGraph();

        private void ExecuteInterruptAlgorithmCommand(object param)
        {
            Statistics.ForEach(stat => stat.TryInterrupt());
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
        {
            return CanExecuteGraphOperation(param) && CanExecuteOperation(param);
        }

        private bool CanExecuteGraphOperation(object param) 
        {
            return !Graph.IsNull();
        }

        private bool CanExecuteOperation(object param) 
        {
            return FinishedAlgorithmsCount == StartedAlgorithmsCount;
        }

        private Tuple<string, BaseAnimationSpeed>[] GetSpeedTupleCollection()
        {
            var enumValues = new EnumValues<AnimationSpeeds>();
            var enumValuesWithoutIgnored = new EnumValuesWithoutIgnored<AnimationSpeeds>(enumValues);
            string Description(AnimationSpeeds speed) => speed.GetDescriptionAttributeValueOrTypeName();
            BaseAnimationSpeed Speed(AnimationSpeeds speed) => speed.GetAttributeOrNull<BaseAnimationSpeed>();
            return enumValuesWithoutIgnored.ToTupleCollection(Description, Speed);
        }

        private readonly Lazy<Tuple<string, BaseAnimationSpeed>[]> animationSpeeds;
    }
}