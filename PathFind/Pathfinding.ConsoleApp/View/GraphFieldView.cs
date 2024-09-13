using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Domain.Interface;
using Pathfinding.Shared.Primitives;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphFieldView : FrameView
    {
        private readonly GraphFieldViewModel viewModel;
        private readonly PathfindingRangeViewModel rangeViewModel;

        private readonly Dictionary<Coordinate, VertexView> viewsMap = new();
        private readonly CompositeDisposable disposables = new();
        private readonly CompositeDisposable vertexDisposables = new();

        public GraphFieldView(GraphFieldViewModel viewModel,
            PathfindingRangeViewModel rangeViewModel,
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
            messenger.Register<OpenAlgorithmRunViewMessage>(this, OnOpenRunViewRecieved);
            messenger.Register<CloseAlgorithmRunViewMessage>(this, OnCloseRunViewMessage);
        }

        private void OnOpenRunViewRecieved(object recipient, OpenAlgorithmRunViewMessage msg)
        {
            Visible = false;
        }

        private void OnCloseRunViewMessage(object recipient, CloseAlgorithmRunViewMessage msg)
        {
            Visible = true;
        }

        private void SubscribeOnRangeExtremumChanging(Expression<Func<PathfindingRangeViewModel, VertexModel>> expression,
            Action<VertexView> action)
        {
            rangeViewModel.WhenAnyValue(expression)
                .Where(x => x != null)
                .Where(x => viewsMap.ContainsKey(x.Position))
                .Do(x => action(viewsMap[x.Position]))
                .Subscribe()
                .DisposeWith(vertexDisposables);
            rangeViewModel.WhenAnyValue(expression)
                .Buffer(count: 2, skip: 1)
                .Where(buffer => buffer[1] == null && buffer[0] != null)
                .Select(buffer => buffer[0])
                .Where(x => viewsMap.ContainsKey(x.Position))
                .Do(x => viewsMap[x.Position].VisualizeAsRegular())
                .Subscribe()
                .DisposeWith(vertexDisposables);
        }

        private void OnTransitAdded(VertexModel vertex)
        {
            if (viewsMap.TryGetValue(vertex.Position, out var view))
            {
                view.VisualizeAsTransit();
            }
        }

        private void OnTransitRemoved(VertexModel vertex)
        {
            if (viewsMap.TryGetValue(vertex.Position, out var view))
            {
                view.VisualizeAsRegular();
            }
        }

        private void SubscribeOnPathfindingRangeChanging()
        {
            rangeViewModel.Transit.ActOnEveryObject(OnTransitAdded, OnTransitRemoved)
                .DisposeWith(vertexDisposables);
            SubscribeOnRangeExtremumChanging(x => x.Source, x => x.VisualizeAsSource());
            SubscribeOnRangeExtremumChanging(x => x.Target, x => x.VisualizeAsTarget());
        }

        private void RenderGraph(IGraph<VertexModel> graph)
        {
            RemoveAll();
            vertexDisposables.Clear();
            viewsMap.Clear();

            foreach (var vertex in graph)
            {
                var view = new VertexView(vertex);
                view.DisposeWith(vertexDisposables);
                viewsMap.Add(vertex.Position, view);
                SubscribeToButton1Clicked(view);
                SubscribeToButton3Clicked(view);
                SubscribeOnWheelButton(view);
            }

            Application.MainLoop.Invoke(() =>
            {
                Add(viewsMap.Values.ToArray());
            });

            SubscribeOnPathfindingRangeChanging();
        }

        private void SubscribeToButton1Clicked(VertexView view)
        {
            view.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Pressed)
                .Select(x => (VertexModel)x.MouseEvent.View.Data)
                .InvokeCommand(rangeViewModel, x => x.AddToRangeCommand)
                .DisposeWith(vertexDisposables);

            view.Events().MouseClick
                .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.Button1Pressed)
                       && x.MouseEvent.Flags.HasFlag(MouseFlags.ButtonCtrl))
                .Select(x => (VertexModel)x.MouseEvent.View.Data)
                .InvokeCommand(rangeViewModel, x => x.RemoveFromRangeCommand)
                .DisposeWith(vertexDisposables);
        }

        private void SubscribeToButton3Clicked(VertexView view)
        {
            view.Events().MouseClick
                .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.Button3Pressed)
                         && x.MouseEvent.Flags.HasFlag(MouseFlags.ButtonCtrl)
                         || x.MouseEvent.Flags == MouseFlags.Button3Clicked)
                .Select(x => (VertexModel)x.MouseEvent.View.Data)
                .InvokeCommand(viewModel, x => x.ReverseVertexCommand)
                .DisposeWith(vertexDisposables);
        }

        private void SubscribeOnWheelButton(VertexView view)
        {
            view.Events().MouseClick
                .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.WheeledDown))
                .Select(x => (VertexModel)x.MouseEvent.View.Data)
                .InvokeCommand(viewModel, x => x.DecreaseVertexCostCommand)
                .DisposeWith(vertexDisposables);
            view.Events().MouseClick
                .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.WheeledUp))
                .Select(x => (VertexModel)x.MouseEvent.View.Data)
                .InvokeCommand(viewModel, x => x.IncreaseVertexCostCommand)
                .DisposeWith(vertexDisposables);
        }
    }
}
