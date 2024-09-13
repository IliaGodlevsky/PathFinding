using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class RunCreationView : FrameView
    {
        public RunCreationView([KeyFilter(KeyFilters.NewRunView)] IEnumerable<Terminal.Gui.View> children,
            [KeyFilter(KeyFilters.Views)] IMessenger messenger)
        {
            Initialize();
            Add(children.ToArray());
            messenger.Register<OpenRunCreationViewMessage>(this, OnOpenRunViewRequest);
            messenger.Register<CloseRunCreationViewMessage>(this, OnClose);
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

        private void OnOpenRunViewRequest(object recipient, OpenRunCreationViewMessage msg)
        {
            Visible = true;
        }

        private void OnClose(object recipient, CloseRunCreationViewMessage msg)
        {
            Visible = false;
        }
    }
}
