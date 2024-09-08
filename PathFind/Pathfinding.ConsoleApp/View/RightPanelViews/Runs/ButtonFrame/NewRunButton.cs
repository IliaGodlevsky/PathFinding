using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View.RightPanelViews.Runs.ButtonFrame
{
    internal sealed partial class NewRunButton : Button
    {
        private readonly IMessenger messenger;

        public NewRunButton([KeyFilter(KeyFilters.Views)] IMessenger messenger)
        {
            Initialize();
            this.messenger = messenger;
            MouseClick += OnNewButtonClicked;
        }

        private void OnNewButtonClicked(MouseEventArgs e)
        {
            messenger.Send(new OpenRunCreateViewRequest());
        }
    }
}
