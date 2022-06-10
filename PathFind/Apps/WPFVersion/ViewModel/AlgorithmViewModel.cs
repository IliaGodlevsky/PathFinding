using Algorithm.Base;
using Algorithm.Interfaces;
using Autofac;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using ValueRange.Enums;
using ValueRange.Extensions;
using WPFVersion.DependencyInjection;
using WPFVersion.Extensions;
using WPFVersion.Infrastructure;
using WPFVersion.Messages.ActionMessages;
using WPFVersion.Messages.DataMessages;
using WPFVersion.ViewModel.BaseViewModels;

namespace WPFVersion.ViewModel
{
    internal class AlgorithmViewModel : NotifyPropertyChanged
    {
        public const string Started = "Started";
        public const string Paused = "Paused";
        public const string Interrupted = "Interrupted";
        public const string Finished = "Finished";
        public const string Failed = "Failed";

        private readonly PathfindingAlgorithm algorithm;
        private readonly IMessenger messenger;

        private int pathLength;
        private double pathCost;
        private int visitedVerticesCount;
        private string status;
        private string time;
        private int delayTime;

        public ICommand InterruptCommand { get; }

        public ICommand RemoveCommand { get; }

        public ICommand PauseCommand { get; }

        public ICommand ResumeCommand { get; }

        public bool IsStarted => algorithm.IsInProcess;

        public IAlgorithm Algorithm => algorithm;

        public int Index { get; }

        public string AlgorithmName { get; }

        public int PathLength { get => pathLength; set => Set(ref pathLength, value); }

        public double PathCost { get => pathCost; set => Set(ref pathCost, value); }

        public int VisitedVerticesCount { get => visitedVerticesCount; set => Set(ref visitedVerticesCount, value); }

        public string Status { get => status; set => Set(ref status, value); }

        public string Time { get => time; set => Set(ref time, value); }

        public int DelayTime
        {
            get => delayTime;
            set
            {
                Set(ref delayTime, (int)Constants.AlgorithmDelayTimeValueRange.ReturnInRange(value, ReturnOptions.Cycle));
                messenger.SendParallel(new DelayTimeChangedMessage(delayTime, Index));
            }
        }

        public AlgorithmViewModel(PathfindingAlgorithm algorithm, int delayTime, int index)
        {
            this.algorithm = algorithm;
            this.delayTime = delayTime;
            AlgorithmName = algorithm.ToString();
            Status = AlgorithmViewModel.Started;
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