using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class PathfindingProcessButtonsFrame : FrameView
    {
        private readonly IMessenger messenger;

        public PathfindingProcessButtonsFrame(
            [KeyFilter(KeyFilters.CreateRunButtonsFrame)] IEnumerable<Terminal.Gui.View> children,
            [KeyFilter(KeyFilters.Views)] IMessenger messenger)
        {
            Initialize();
            Add(children.ToArray());
            this.messenger = messenger;
            messenger.Register<OpenAlgorithmCreationViewMessage>(this, OnOpenRunCreationView);
            messenger.Register<CloseAlgorithmCreationViewMessage>(this, OnCloseCreateRunView);
        }

        private void OnOpenRunCreationView(object recipient, OpenAlgorithmCreationViewMessage msg)
        {
            Visible = true;
        }

        private void OnCloseCreateRunView(object recipient, CloseAlgorithmCreationViewMessage msg)
        {
            Visible = false;
        }
    }
}
