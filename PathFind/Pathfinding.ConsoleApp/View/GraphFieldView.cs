using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Shared.Extensions;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphFieldView : FrameView
    {
        private const int LabelWidth = 3;

        private readonly GraphFieldViewModel viewModel;

        private readonly CompositeDisposable disposables = new();
        private readonly CompositeDisposable vertexDisposables = new();

        public GraphFieldView(GraphFieldViewModel viewModel)
        {
            this.viewModel = viewModel;
            Initialize();
            this.viewModel.WhenAnyValue(x => x.Graph)
                .Where(x => x != null)
                .DistinctUntilChanged()
                .Do(RenderGraph)
                .Subscribe()
                .DisposeWith(disposables);
        }

        private void RenderGraph(IGraph<VertexViewModel> graph)
        {
            vertexDisposables.Clear();
            RemoveAll();
            foreach (var vertex in graph)
            {
                var label = CreateVertexView(vertex);
                SubscribeToButton1Clicked(label);
                SubscribeToButton3Clicked(label);
                label.DisposeWith(vertexDisposables);
                Add(label);
            }
            SetNeedsDisplay();
        }

        private void SubscribeToButton1Clicked(VertexView view)
        {
            view.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Clicked)
                .InvokeCommand(viewModel, x => x.AddInRangeCommand)
                .DisposeWith(vertexDisposables);
        }

        private void SubscribeToButton3Clicked(VertexView view)
        {
            view.Events().MouseClick
                .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.Button3Pressed)
                         && x.MouseEvent.Flags.HasFlag(MouseFlags.ButtonCtrl)
                         || x.MouseEvent.Flags == MouseFlags.Button3Clicked)
                .InvokeCommand(viewModel, x => x.ReverseVertexCommand)
                .DisposeWith(vertexDisposables);
        }

        private VertexView CreateVertexView(VertexViewModel vertex)
        {
            return new(vertex)
            {
                X = vertex.Position.GetX() * LabelWidth,
                Y = vertex.Position.GetY(),
                Width = LabelWidth,
                Text = vertex.Cost.CurrentCost.ToString(),
                Data = vertex
            };
        }
    }
}
