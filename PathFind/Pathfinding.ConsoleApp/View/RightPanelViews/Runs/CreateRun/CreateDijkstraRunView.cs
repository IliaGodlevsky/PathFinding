using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.ViewModel;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View.RightPanelViews.Runs.CreateRun
{
    internal sealed class CreateDijkstraRunView : FrameView
    {
        private readonly IEnumerable<Terminal.Gui.View> children;
        private readonly IMessenger messenger;
        private readonly CreateDijkstraRunViewModel viewModel;

        public CreateDijkstraRunView([KeyFilter(KeyFilters.CreateAlgorithmRunView)]IEnumerable<Terminal.Gui.View> children,
            [KeyFilter(KeyFilters.Views)] IMessenger messenger,
            CreateDijkstraRunViewModel viewModel)
        {
            X = Pos.Percent(30) + 1;
            Y = 1;
            Width = Dim.Fill();
            Height = Dim.Percent(80);
            Visible = false;
            this.children = children;
            this.messenger = messenger;
            this.viewModel = viewModel;
            Add(children.ToArray());
            messenger.Register<IOpenViewMessage>(this, OnOpen);
        }

        private void OnOpen(object recipient, IOpenViewMessage request)
        {
            request.Open(this);
        }
    }
}
