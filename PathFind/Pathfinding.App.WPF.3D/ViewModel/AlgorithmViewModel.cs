using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.App.WPF._3D.DependencyInjection;
using Pathfinding.App.WPF._3D.Infrastructure.Commands;
using Pathfinding.App.WPF._3D.Messages.PassValueMessages;
using Pathfinding.App.WPF._3D.ViewModel.BaseViewModel;
using System;
using System.Windows.Input;

namespace Pathfinding.App.WPF._3D.ViewModel
{
    internal class AlgorithmViewModel : NotifyPropertyChanged
    {
        public const string Paused = "Paused";
        public const string Interrupted = "Interrupted";
        public const string Finished = "Finished";
        public const string Failed = "Failed";
        public const string Started = "Started";

        private readonly PathfindingProcess algorithm;
        private readonly IMessenger messenger;

        private string time;
        private int pathLength;
        private double pathCost;
        private int visitedCount;
        private string status;

        public ICommand InterruptCommand { get; }

        public ICommand RemoveCommand { get; }

        public ICommand PauseCommand { get; }

        public ICommand ResumeCommand { get; }

        public Guid Id { get; }

        public bool IsStarted => algorithm.IsInProcess;

        public string AlgorithmName { get; }

        public bool IsInterrupted => Status == Interrupted;

        public string Time { get => time; set => Set(ref time, value); }

        public int PathLength { get => pathLength; set => Set(ref pathLength, value); }

        public double PathCost { get => pathCost; set => Set(ref pathCost, value); }

        public int VisitedVerticesCount { get => visitedCount; set => Set(ref visitedCount, value); }

        public string Status { get => status; set => Set(ref status, value); }

        public AlgorithmViewModel(PathfindingProcess algorithm)
        {
            this.algorithm = algorithm;
            Id = algorithm.Id;
            messenger = DI.Container.Resolve<IMessenger>();
            AlgorithmName = algorithm.ToString();
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
            messenger.Send(new RemoveAlgorithmMessage(Id));
        }

        private bool CanExecuteRemoveCommand(object param)
        {
            return !algorithm.IsInProcess;
        }
    }
}