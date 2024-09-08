using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View.RightPanelViews.Runs.CreateRun
{
    internal sealed partial class CreateRunView : FrameView
    {
        private readonly IEnumerable<Terminal.Gui.View> children;
        private readonly IMessenger messenger;

        public CreateRunView([KeyFilter(KeyFilters.CreateRunView)]IEnumerable<Terminal.Gui.View> children,
            [KeyFilter(KeyFilters.Views)] IMessenger messenger)
        {
            Initialize();
            Add(children.ToArray());
            this.children = children;
            this.messenger = messenger;
            messenger.Register<OpenRunCreateViewRequest>(this, OnOpenRunViewRequest);
        }

        private void Initialize()
        {
            X = Pos.Center();
            Y = Pos.Center();
            Width = Dim.Fill();
            Height = Dim.Fill();
            Visible = false;
            Border = new Border()
            {
                BorderStyle = BorderStyle.None,
                BorderThickness = new Thickness(0)
            };
            Visible = false;
        }

        private void OnOpenRunViewRequest(object recipient, OpenRunCreateViewRequest msg)
        {
            Visible = true;
        }
    }
}
