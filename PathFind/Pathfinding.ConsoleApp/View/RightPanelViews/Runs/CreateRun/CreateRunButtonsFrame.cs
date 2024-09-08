using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View.RightPanelViews.Runs.CreateRun
{
    internal sealed partial class CreateRunButtonsFrame : FrameView
    {
        private readonly IMessenger messenger;

        public CreateRunButtonsFrame([KeyFilter(KeyFilters.CreateRunButtonsFrame)]IEnumerable<Terminal.Gui.View> children,
            [KeyFilter(KeyFilters.Views)]IMessenger messenger)
        {
            Initialize();
            Add(children.ToArray());
            this.messenger = messenger;
            messenger.Register<IOpenViewMessage>(this, OnOpen);
        }

        private void OnOpen(object recipient, IOpenViewMessage msg)
        {
            Visible = true;
        }
    }
}
