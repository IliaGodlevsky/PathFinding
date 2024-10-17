using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class AlgorithmCreationView : FrameView
    {
        public AlgorithmCreationView(
            [KeyFilter(KeyFilters.AlgorithmCreationView)] IEnumerable<Terminal.Gui.View> children,
            [KeyFilter(KeyFilters.Views)] IMessenger messenger)
        {
            Initialize();
            Add(children.ToArray());
            messenger.Register<OpenAlgorithmCreationViewMessage>(this, OnOpenAlgorithmParametresView);
            messenger.Register<CloseAlgorithmCreationViewMessage>(this, OnCloseAlgorithmParametresView);
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

        private void OnOpenAlgorithmParametresView(object recipient, OpenAlgorithmCreationViewMessage msg)
        {
            Visible = true;
        }

        private void OnCloseAlgorithmParametresView(object recipient, CloseAlgorithmCreationViewMessage msg)
        {
            Visible = false;
        }
    }
}
