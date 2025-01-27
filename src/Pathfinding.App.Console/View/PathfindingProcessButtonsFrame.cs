using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Injection;
using Pathfinding.App.Console.Messages.View;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed partial class PathfindingProcessButtonsFrame : FrameView
    {
        public PathfindingProcessButtonsFrame(
            [KeyFilter(KeyFilters.CreateRunButtonsFrame)] IEnumerable<Terminal.Gui.View> children,
            [KeyFilter(KeyFilters.Views)] IMessenger messenger)
        {
            Initialize();
            Add(children.ToArray());
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
