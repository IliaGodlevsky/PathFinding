﻿using Algorithm.Infrastructure.EventArguments;
using Common.Extensions;
using Common.Interface;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
using Logging.Interface;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFVersion3D.Enums;
using WPFVersion3D.Infrastructure;
using WPFVersion3D.Messages;

namespace WPFVersion3D.ViewModel
{
    internal class PathFindingViewModel : PathFindingModel, IViewModel, INotifyPropertyChanged
    {
        public event EventHandler WindowClosed;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand ConfirmPathFindAlgorithmChoice { get; }
        public ICommand CancelPathFindAlgorithmChoice { get; }

        public PathFindingViewModel(ILog log, IMainModel model, BaseEndPoints endPoints)
            : base(log, model, endPoints)
        {
            ConfirmPathFindAlgorithmChoice = new RelayCommand(
                ExecuteConfirmPathFindAlgorithmChoice,
                CanExecuteConfirmPathFindAlgorithmChoice);
            CancelPathFindAlgorithmChoice = new RelayCommand(ExecuteCloseWindowCommand);
            Messenger.Default.Register<AlgorithmStatisticsIndexMessage>(this, Constants.MessageToken, SetAlgorithmIndex);
        }

        private void SetAlgorithmIndex(AlgorithmStatisticsIndexMessage message)
        {
            Messenger.Default.Unregister<AlgorithmStatisticsIndexMessage>(this, Constants.MessageToken, SetAlgorithmIndex);
            AlgorithmStatisticsIndex = message.Index;
        }

        protected override void OnAlgorithmStarted(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmStarted(sender, e);
            string algorithmName = Algorithm.GetDescriptionAttributeValueOrTypeName();
            var message = new AlgorithmStartedMessage(algorithm, algorithmName);
            Messenger.Default.Send(message, Constants.MessageToken);
        }

        protected override void Summarize()
        {
            var status = path.PathLength > 0 ? AlgorithmStatuses.Finished : AlgorithmStatuses.Failed;
            string time = timer.Elapsed.ToString(@"mm\:ss\.ff");
            var message = new UpdateAlgorithmStatisticsMessage(AlgorithmStatisticsIndex, time, visitedVerticesCount,
                status, path.PathLength, path.PathCost);
            Messenger.Default.Send(message, Constants.MessageToken);
        }

        protected override async void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            timer.Wait(DelayTime);
            string time = timer.Elapsed.ToString(@"mm\:ss\.ff");
            var message = new UpdateAlgorithmStatisticsMessage(AlgorithmStatisticsIndex, time, visitedVerticesCount);
            Messenger.Default.Send(message, Constants.MessageToken);
            await Task.Run(() => base.OnVertexVisited(sender, e));
        }

        protected override async void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            await Task.Run(() => base.OnVertexEnqueued(sender, e));
        }

        protected override void OnAlgorithmFinished(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmFinished(sender, e);
            Messenger.Default.Send(new AlgorithmFinishedMessage(AlgorithmStatisticsIndex), Constants.MessageToken);
        }

        private void ExecuteCloseWindowCommand(object param)
        {
            WindowClosed?.Invoke(this, EventArgs.Empty);
            WindowClosed = null;
        }

        private void ExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            ExecuteCloseWindowCommand(null);
            base.FindPath();
        }

        private bool CanExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            return Algorithms.Any(item => item.Item2 == Algorithm);
        }

        private int AlgorithmStatisticsIndex { get; set; }
    }
}
