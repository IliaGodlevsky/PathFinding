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
    internal sealed partial class AlgorithmRunFieldView : FrameView
    {
        private const int IntialSpeed = 75;

        private readonly CompositeDisposable vertexDisposables = new();
        private readonly CompositeDisposable disposables = new();

        private readonly AlgorithmRunFieldViewModel viewModel;

        private int VerticesToVisualizePerTime { get; set; } = IntialSpeed;

        public AlgorithmRunFieldView(AlgorithmRunFieldViewModel viewModel,
            [KeyFilter(KeyFilters.Views)] IMessenger messenger)
        {
            Visible = false;
            X = 0;
            Y = 0;
            Width = Dim.Percent(67);
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
            messenger.Register<CloseAlgorithmRunFieldViewMessage>(this, OnCloseAlgorithmViewMessage);
        }

        private void OnOpen(object recipient, OpenAlgorithmRunViewMessage msg)
        {
            Visible = true;
        }

        private void OnCloseAlgorithmViewMessage(object recipient, CloseAlgorithmRunFieldViewMessage msg)
        {
            Visible = false;
            Application.MainLoop.Invoke(RemoveAll);
            vertexDisposables.Clear();
        }

        private void RenderGraphState(IReadOnlyCollection<RunVertexModel> graphState)
        {
            RemoveAll();
            vertexDisposables.Clear();
            var children = new List<RunVertexView>(graphState.Count);
            VerticesToVisualizePerTime = IntialSpeed;

            foreach (var vertex in graphState)
            {
                var view = new RunVertexView(vertex);
                view.Events().MouseClick
                    .Do(async x => await Visualize())
                    .Subscribe()
                    .DisposeWith(vertexDisposables);
                children.Add(view);
                view.DisposeWith(vertexDisposables);
            }

            Application.MainLoop.Invoke(() =>
            {
                Add(children.ToArray());
            });
        }

        private async Task Visualize()
        {
            var command = viewModel.VisualizeNextCommand;
            while (viewModel.Remained > 0)
            {
                await command.Execute(VerticesToVisualizePerTime);
                VerticesToVisualizePerTime++;
                Application.Refresh();
            }
        }
    }
}
