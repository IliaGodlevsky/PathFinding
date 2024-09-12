using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class AlgorithmSettingsView : FrameView
    {
        private readonly IMessenger messenger;

        public AlgorithmSettingsView([KeyFilter(KeyFilters.AlgorithmParametresView)] IEnumerable<Terminal.Gui.View> children,
            [KeyFilter(KeyFilters.Views)] IMessenger messenger)
        {
            X = Pos.Percent(30);
            Y = 0;
            Width = Dim.Fill();
            Height = Dim.Percent(90);
            Visible = false;
            Border = new();
            this.messenger = messenger;
            Add(children.ToArray());
            messenger.Register<OpenRunCreationViewMessage>(this, OnOpenRunCreationView);
            messenger.Register<CloseRunCreationViewMessage>(this, OnCloseRunCreationView);
        }

        private void OnOpenRunCreationView(object recipient, OpenRunCreationViewMessage request)
        {
            Visible = true;
        }

        private void OnCloseRunCreationView(object recipient, CloseRunCreationViewMessage msg)
        {
            Visible = false;
        }
    }
}
