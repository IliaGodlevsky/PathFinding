﻿using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.WPF._2D.Infrastructure;
using Pathfinding.App.WPF._2D.Messages.DataMessages;
using Pathfinding.App.WPF._2D.Model;
using System.Windows.Input;
using WPFVersion.DependencyInjection;

namespace Pathfinding.App.WPF._2D.ViewModel
{
    internal class ColorizeGraphViewModel
    {
        private readonly IMessenger messenger;
        private CostColors colors;

        private bool IsAllAlgorithmsFinished { get; set; }

        public ICommand ColorizeCommand { get; }

        public ICommand ResetColorizingCommand { get; }

        public ColorizeGraphViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
            messenger.Register<IsAllAlgorithmsFinishedMessage>(this, OnAllAlgorithmsFinished);
            ColorizeCommand = new RelayCommand(ExecuteColorizeCommand, CanExecuteColorizeCommand);
            ResetColorizingCommand = new RelayCommand(ExecuteRestoreColorsCommand, CanExecuteColorizeCommand);
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            colors = new CostColors(message.Graph);
        }

        private void OnAllAlgorithmsFinished(IsAllAlgorithmsFinishedMessage message)
        {
            IsAllAlgorithmsFinished = message.IsAllAlgorithmsFinished;
        }

        private void ExecuteColorizeCommand(object param)
        {
            colors.ColorizeAccordingToCost();
        }

        private void ExecuteRestoreColorsCommand(object param)
        {
            colors.ReturnPreviousColors();
        }

        private bool CanExecuteColorizeCommand(object param)
        {
            return colors != null && IsAllAlgorithmsFinished;
        }
    }
}