﻿using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Shared;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    [Order(14)]
    internal sealed class BidirectAStarAlgorithmView : Label
    {
        private readonly IMessenger messenger;
        private readonly BidirectAStarAlgorithmViewModel viewModel;

        public BidirectAStarAlgorithmView(
            [KeyFilter(KeyFilters.Views)] IMessenger messenger,
            BidirectAStarAlgorithmViewModel viewModel)
        {
            Text = viewModel.AlgorithmId;
            Y = 1;
            X = 0;
            this.messenger = messenger;
            this.viewModel = viewModel;
            MouseClick += OnViewClicked;
        }

        private void OnViewClicked(MouseEventArgs e)
        {
            if (e.MouseEvent.Flags == MouseFlags.Button1Clicked)
            {
                messenger.Send(new OpenStepRuleViewMessage());
                messenger.Send(new OpenHeuristicsViewMessage());
                messenger.Send(new CloseSpreadViewMessage());
                messenger.Send(new StepRuleViewModelChangedMessage(viewModel));
                messenger.Send(new HeuristicsViewModelChangedMessage(viewModel));
                messenger.Send(new PathfindingViewModelChangedMessage(viewModel));
            }
        }
    }
}
