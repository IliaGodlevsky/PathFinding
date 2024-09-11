using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Shared.Primitives;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using System.Threading.Tasks;
using Pathfinding.ConsoleApp.Messages.View;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class AlgorithmRunView : FrameView
    {
        private static readonly InclusiveValueRange<int> renderRange = (1000, 30);

        private readonly CompositeDisposable vertexDisposables = new();
        private readonly CompositeDisposable disposables = new();

        private readonly AlgorithmRunViewModel viewModel;
        private readonly IMessenger messenger;

        private int RenderSpeed { get; set; } = 45;

        public AlgorithmRunView(AlgorithmRunViewModel viewModel,
            [KeyFilter(KeyFilters.Views)]IMessenger messenger)
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
            this.messenger = messenger;
            viewModel.WhenAnyValue(x => x.GraphState)
                .Do(RenderGraphState)
                .Subscribe()
                .DisposeWith(disposables);
            messenger.Register<OpenRunViewMessage>(this, OnOpen);
            messenger.Register<CloseAlgorithmViewMessage>(this, OnCloseAlgorithmViewMessage);
        }

        private void OnOpen(object recipient, OpenRunViewMessage msg)
        {
            Visible = true;
        }

        private void OnCloseAlgorithmViewMessage(object recipient, CloseAlgorithmViewMessage msg)
        {
            Visible = false;
            RemoveAll();
            vertexDisposables.Clear();
            viewModel.VisualizationPipeline.Clear();
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

            Add(children.ToArray());

            var canCreate = viewModel.VisualizeNextCommand.CanExecute;

            this.Events()
                .MouseEnter
                .Do(async x => await Visualize(x))
                .Subscribe()
                .DisposeWith(vertexDisposables);
        }

        private async Task Visualize(MouseEventArgs e)
        {
            int i = 0;
            while (viewModel.Remained > 0)
            {
                await viewModel.VisualizeNextCommand.Execute(e);
                if (++i % RenderSpeed == 0)
                {
                    Application.Refresh();
                }
            }
            Application.Refresh();
        }
    }
}
