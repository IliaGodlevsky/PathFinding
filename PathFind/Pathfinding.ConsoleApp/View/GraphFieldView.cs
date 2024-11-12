using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphFieldView : FrameView
    {
        private readonly IGraphFieldViewModel viewModel;
        private readonly IPathfindingRangeViewModel rangeViewModel;

        private readonly CompositeDisposable disposables = new();
        private readonly CompositeDisposable vertexDisposables = new();

        private readonly Terminal.Gui.View container = new();

        public GraphFieldView(IGraphFieldViewModel viewModel,
            IPathfindingRangeViewModel rangeViewModel,
            [KeyFilter(KeyFilters.Views)] IMessenger messenger)
        {
            this.viewModel = viewModel;
            this.rangeViewModel = rangeViewModel;
            Initialize();
            this.viewModel.WhenAnyValue(x => x.Graph)
                .Where(x => x != null)
                .Do(RenderGraph)
                .Subscribe()
                .DisposeWith(disposables);
            messenger.Register<OpenAlgorithmRunViewMessage>(this, OnOpenAlgorithmRunView);
            messenger.Register<CloseAlgorithmRunFieldViewMessage>(this, OnCloseAlgorithmRunField);
            container.X = Pos.Center();
            container.Y = Pos.Center();
            Add(container);
        }

        private void OnOpenAlgorithmRunView(object recipient, OpenAlgorithmRunViewMessage msg)
        {
            Application.MainLoop.Invoke(() =>
            {
                Visible = false;
            });
        }

        private void OnCloseAlgorithmRunField(object recipient, CloseAlgorithmRunFieldViewMessage msg)
        {
            Application.MainLoop.Invoke(() =>
            {
                Visible = true;
            });
        }

        private void RenderGraph(IGraph<GraphVertexModel> graph)
        {
            Application.MainLoop.Invoke(() =>
            {
                container.RemoveAll();
            });
            vertexDisposables.Clear();

            var views = new List<GraphVertexView>();
            foreach (var vertex in graph)
            {
                var view = new GraphVertexView(vertex);
                view.DisposeWith(vertexDisposables);
                SubscribeToButton1Clicked(view, vertex);
                SubscribeToButton3Clicked(view, vertex);
                SubscribeOnWheelButton(view, vertex);
                views.Add(view);
            }
            Application.MainLoop.Invoke(() =>
            {
                container.Add(views.ToArray());
                container.Width = graph.GetWidth() * 3;
                container.Height = graph.GetLength();
            });
        }

        private void SubscribeToButton1Clicked(GraphVertexView view, GraphVertexModel model)
        {
            view.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Pressed)
                .Select(x => model)
                .InvokeCommand(rangeViewModel, x => x.AddToRangeCommand)
                .DisposeWith(vertexDisposables);

            view.Events().MouseClick
                .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.Button1Pressed)
                       && x.MouseEvent.Flags.HasFlag(MouseFlags.ButtonCtrl))
                .Select(x => model)
                .InvokeCommand(rangeViewModel, x => x.RemoveFromRangeCommand)
                .DisposeWith(vertexDisposables);

            view.Events().MouseClick
                .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.Button1DoubleClicked)
                       && x.MouseEvent.Flags.HasFlag(MouseFlags.ButtonCtrl)
                       && x.MouseEvent.Flags.HasFlag(MouseFlags.ButtonAlt))
                .Select(x => Unit.Default)
                .InvokeCommand(rangeViewModel, x => x.DeletePathfindingRange)
                .DisposeWith(vertexDisposables);
        }

        private void SubscribeToButton3Clicked(GraphVertexView view, GraphVertexModel model)
        {
            view.Events().MouseClick
                .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.Button3Pressed)
                         && x.MouseEvent.Flags.HasFlag(MouseFlags.ButtonCtrl)
                         || x.MouseEvent.Flags == MouseFlags.Button3Clicked)
                .Select(x => model)
                .InvokeCommand(viewModel, x => x.ReverseVertexCommand)
                .DisposeWith(vertexDisposables);
        }

        private void SubscribeOnWheelButton(GraphVertexView view, GraphVertexModel model)
        {
            view.Events().MouseClick
                .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.WheeledDown))
                .Select(x => model)
                .InvokeCommand(viewModel, x => x.DecreaseVertexCostCommand)
                .DisposeWith(vertexDisposables);
            view.Events().MouseClick
                .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.WheeledUp))
                .Select(x => model)
                .InvokeCommand(viewModel, x => x.IncreaseVertexCostCommand)
                .DisposeWith(vertexDisposables);
        }
    }
}
