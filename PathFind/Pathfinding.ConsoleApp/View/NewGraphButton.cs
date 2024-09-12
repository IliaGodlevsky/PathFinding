using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class NewGraphButton : Button
    {
        private readonly IMessenger messenger;

        public NewGraphButton([KeyFilter(KeyFilters.Views)] IMessenger messenger)
        {
            this.messenger = messenger;
            Initialize();
            MouseClick += NewGraphButtonClicked;
        }

        private void NewGraphButtonClicked(MouseEventArgs e)
        {
            if (e.MouseEvent.Flags == MouseFlags.Button1Clicked)
            {
                messenger.Send(new OpenGraphCreateViewRequest());
            }
        }
    }
}
