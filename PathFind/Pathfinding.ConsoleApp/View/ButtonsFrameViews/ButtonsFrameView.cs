using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View.ButtonsFrameViews
{
    internal sealed partial class ButtonsFrameView : FrameView
    {
        private readonly IMessenger messenger;

        public ButtonsFrameView([KeyFilter(KeyFilters.GraphTableButtons)]IEnumerable<Terminal.Gui.View> children, 
            [KeyFilter(KeyFilters.Views)]IMessenger messenger)
        {
            this.messenger = messenger;
            Initialize();
            Add(children.ToArray());
        }
    }
}
