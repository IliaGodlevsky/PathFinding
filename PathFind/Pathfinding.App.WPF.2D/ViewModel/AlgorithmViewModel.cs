using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.App.WPF._2D.Extensions;
using Pathfinding.App.WPF._2D.Infrastructure;
using Pathfinding.App.WPF._2D.Messages.ActionMessages;
using Pathfinding.App.WPF._2D.Messages.DataMessages;
using Pathfinding.App.WPF._2D.ViewModel.BaseViewModels;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange.Enums;
using System;
using System.Windows.Input;
using WPFVersion.DependencyInjection;

namespace Pathfinding.App.WPF._2D.ViewModel
{
    internal class AlgorithmViewModel : NotifyPropertyChanged
    {
        public const string Started = "Started";
        public const string Paused = "Paused";
        public const string Interrupted = "Interrupted";
        public const string Finished = "Finished";
        public const string Failed = "Failed";

        private readonly PathfindingProcess algorithm;
        private readonly IMessenger messenger;

        private int pathLength;
        private double pathCost;
        private int visitedVerticesCount;
        private string status;
        private string time;
        private TimeSpan delayTime;

        public ICommand InterruptCommand { get; }

        public ICommand RemoveCommand { get; }

        public ICommand PauseCommand { get; }

        public ICommand ResumeCommand { get; }

        public bool IsStarted => algorithm.IsInProcess;

        public IAlgorithm<IGraphPath> Algorithm => algorithm;

        public int Index { get; }

        public string AlgorithmName { get; }

        public bool IsInterrupted => Status == Interrupted;

        public int PathLength { get => pathLength; set => Set(ref pathLength, value); }

        public double PathCost { get => pathCost; set => Set(ref pathCost, value); }

        public int VisitedVerticesCount { get => visitedVerticesCount; set => Set(ref visitedVerticesCount, value); }

        public string Status { get => status; set => Set(ref status, value); }

        public string Time { get => time; set => Set(ref time, value); }

        public TimeSpan DelayTime
        {
            get => delayTime;
            set
            {
                Set(ref delayTime, Constants.AlgorithmDelayTimeValueRange.ReturnInRange(value, ReturnOptions.Cycle));
                messenger.SendParallel(new DelayTimeChangedMessage(delayTime, Index));
            }
        }

        public AlgorithmViewModel(PathfindingProcess algorithm, TimeSpan delayTime, int index)
        {
            this.algorithm = algorithm;
            this.delayTime = delayTime;
            AlgorithmName = algorithm.ToString();
            Status = Started;
            Index = index;
            messenger = DI.Container.Resolve<IMessenger>();
            InterruptCommand = new InterruptAlgorithmCommand(algorithm);
            PauseCommand = new PauseAlgorithmCommand(algorithm);
            ResumeCommand = new ResumeAlgorithmCommand(algorithm);
            RemoveCommand = new RelayCommand(ExecuteRemoveCommand, CanExecuteRemoveCommand);
        }

        public AlgorithmViewModel(AlgorithmStartedMessage message, int index)
            : this(message.Algorithm, message.DelayTime, index)
        {

        }

        public void Interrupt()
        {
            algorithm.Interrupt();
        }

        public void Pause()
        {
            algorithm.Pause();
        }

        public void Resume()
        {
            algorithm.Resume();
        }

        private void ExecuteRemoveCommand(object param)
        {
            messenger.Send(new RemoveAlgorithmMessage(this));
        }

        private bool CanExecuteRemoveCommand(object param)
        {
            return !algorithm.IsInProcess;
        }
    }
}