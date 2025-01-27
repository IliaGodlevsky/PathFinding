﻿using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Injection;
using Pathfinding.App.Console.Messages.View;
using Pathfinding.App.Console.Resources;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed partial class CloseAlgorithmCreationButton : Button
    {
        private readonly IMessenger messenger;

        public CloseAlgorithmCreationButton([KeyFilter(KeyFilters.Views)] IMessenger messenger)
        {
            X = Pos.Percent(65);
            Y = 0;
            Text = Resource.CloseAlgorithms;
            this.messenger = messenger;
            MouseClick += OnClicked;
        }

        private void OnClicked(MouseEventArgs e)
        {
            if (e.MouseEvent.Flags == MouseFlags.Button1Clicked)
            {
                messenger.Send(new CloseAlgorithmCreationViewMessage());
            }
        }
    }
}
