﻿using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.WPF._2D.Infrastructure;
using Pathfinding.App.WPF._2D.Messages.ActionMessages;
using Pathfinding.App.WPF._2D.Messages.DataMessages;
using Pathfinding.App.WPF._2D.Model;
using Pathfinding.App.WPF._2D.View;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Visualization.Core.Abstractions;
using System.Windows.Input;
using WPFVersion.DependencyInjection;

namespace Pathfinding.App.WPF._2D.ViewModel.ButtonViewModels
{
    internal class CreateGraphViewModel
    {
        private readonly IMessenger messenger;

        public ICommand CreateGraphCommand { get; }

        private Graph2D<Vertex> Graph { get; set; } = Graph2D<Vertex>.Empty;

        private bool IsAllAlgorithmsFinishedPathfinding { get; set; } = true;

        public CreateGraphViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<IsAllAlgorithmsFinishedMessage>(this, OnAllAlgorithmFinished);
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
            CreateGraphCommand = new RelayCommand(ExecuteCreateGraphCommand, CanExecuteCreateGraphCommand);
        }

        private void ExecuteCreateGraphCommand(object param)
        {
            var window = DI.Container.Resolve<GraphCreateWindow>();
            window.Show();
        }

        private bool CanExecuteCreateGraphCommand(object param)
        {
            return IsAllAlgorithmsFinishedPathfinding;
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            var subscription = DI.Container.Resolve<IGraphSubscription<Vertex>>();
            var adapter = DI.Container.Resolve<PathfindingRangeAdapter<Vertex>>();
            subscription.Unsubscribe(Graph);
            adapter.Undo();
            Graph = message.Graph;
            subscription.Subscribe(Graph);
            messenger.Send(new ClearStatisticsMessage());
        }

        private void OnAllAlgorithmFinished(IsAllAlgorithmsFinishedMessage message)
        {
            IsAllAlgorithmsFinishedPathfinding = message.IsAllAlgorithmsFinished;
        }
    }
}