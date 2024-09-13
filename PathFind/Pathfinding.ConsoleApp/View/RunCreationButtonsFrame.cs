using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class RunCreationButtonsFrame : FrameView
    {
        private readonly IMessenger messenger;

        public RunCreationButtonsFrame([KeyFilter(KeyFilters.CreateRunButtonsFrame)] IEnumerable<Terminal.Gui.View> children,
            [KeyFilter(KeyFilters.Views)] IMessenger messenger)
        {
            Initialize();
            Add(children.ToArray());
            this.messenger = messenger;
            messenger.Register<OpenRunCreationViewMessage>(this, OnOpenRunCreationView);
            messenger.Register<CloseRunCreationViewMessage>(this, OnCloseCreateRunView);
        }

        private void OnOpenRunCreationView(object recipient, OpenRunCreationViewMessage msg)
        {
            Visible = true;
        }

        private void OnCloseCreateRunView(object recipient, CloseRunCreationViewMessage msg)
        {
            Visible = false;
        }
    }
}
