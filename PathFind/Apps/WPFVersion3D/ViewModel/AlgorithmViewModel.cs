using Algorithm.Base;
using Algorithm.Interfaces;
using Autofac;
using Common.Extensions;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Enums;
using WPFVersion3D.Infrastructure.Commands;
using WPFVersion3D.Messages.PassValueMessages;
using WPFVersion3D.ViewModel.BaseViewModel;

namespace WPFVersion3D.ViewModel
{
    internal class AlgorithmViewModel : NotifyPropertyChanged
    {
        private readonly PathfindingAlgorithm algorithm;
        private readonly IMessenger messenger;

        private string time;
        private int pathLength;
        private double pathCost;
        private int visitedCount;
        private AlgorithmStatuses status;

        public ICommand InterruptCommand { get; }

        public ICommand RemoveCommand { get; }

        public ICommand PauseCommand { get; }

        public ICommand ResumeCommand { get; }

        public IAlgorithm Algorithm => algorithm;

        public bool IsStarted => algorithm.IsInProcess;

        public string AlgorithmName { get; }

        public string Time { get => time; set => Set(ref time, value); }

        public int PathLength { get => pathLength; set => Set(ref pathLength, value); }

        public double PathCost { get => pathCost; set => Set(ref pathCost, value); }

        public int VisitedVerticesCount { get => visitedCount; set => Set(ref visitedCount, value); }

        public AlgorithmStatuses Status
        {
            get => status;
            set
            {
                if (status != AlgorithmStatuses.Interrupted)
                {
                    Set(ref status, value);
                }
            }
        }

        public AlgorithmViewModel(PathfindingAlgorithm algorithm)
        {
            this.algorithm = algorithm;
            messenger = DI.Container.Resolve<IMessenger>();
            AlgorithmName = algorithm.GetDescription();
            InterruptCommand = new InterruptAlgorithmCommand(algorithm);
            PauseCommand = new PauseAlgorithmCommand(algorithm);
            ResumeCommand = new ResumeAlgorithmCommand(algorithm);
            RemoveCommand = new RelayCommand(ExecuteRemoveCommand, CanExecuteRemoveCommand);
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