using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class AlgorithmRunView : FrameView
    {
        private readonly CompositeDisposable vertexDisposables = new();
        private readonly CompositeDisposable disposables = new();

        private readonly AlgorithmRunViewModel viewModel;

        private int VerticesToVisualizePerTime { get; set; } = 75;

        public AlgorithmRunView(AlgorithmRunViewModel viewModel,
            [KeyFilter(KeyFilters.Views)] IMessenger messenger)
        {
            Visible = false;
            X = 0;
            Y = 0;
            Width = Dim.Percent(75);
            Height = Dim.Fill();
            Border = new Border()
            {
                BorderBrush = Color.BrightYellow,
                BorderStyle = BorderStyle.Rounded,
                Title = "Run field"
            };
            this.viewModel = viewModel;
            viewModel.WhenAnyValue(x => x.GraphState)
                .Do(RenderGraphState)
                .Subscribe()
                .DisposeWith(disposables);
            messenger.Register<OpenAlgorithmRunViewMessage>(this, OnOpen);
            messenger.Register<CloseAlgorithmRunViewMessage>(this, OnCloseAlgorithmViewMessage);
        }

        private void OnOpen(object recipient, OpenAlgorithmRunViewMessage msg)
        {
            Visible = true;
        }

        private void OnCloseAlgorithmViewMessage(object recipient, CloseAlgorithmRunViewMessage msg)
        {
            Visible = false;
            RemoveAll();
            vertexDisposables.Clear();
            viewModel.Vertices.Clear();
        }

        private void RenderGraphState(IReadOnlyCollection<RunVertexModel> graphState)
        {
            RemoveAll();
            vertexDisposables.Clear();

            var children = new List<RunVertexView>(graphState.Count);
            foreach (var vertex in graphState)
            {
                var view = new RunVertexView(vertex);
                children.Add(view);
                view.DisposeWith(vertexDisposables);
            }

            Application.MainLoop.Invoke(() =>
            {
                Add(children.ToArray());
            });

            this.Events().MouseEnter
                .Do(async x => await Visualize())
                .Subscribe()
                .DisposeWith(vertexDisposables);
        }

        private async Task Visualize()
        {
            var command = viewModel.VisualizeNextCommand;
            while (viewModel.Vertices.Count > 0)
            {
                await command.Execute(VerticesToVisualizePerTime);
                Application.Refresh();
            }
        }
    }
}
