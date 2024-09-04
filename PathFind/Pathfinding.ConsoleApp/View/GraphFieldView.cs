using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Domain.Interface;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using Pathfinding.Shared.Primitives;
using System.Collections.Generic;
using System.Linq.Expressions;
using Pathfinding.ConsoleApp.Model;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphFieldView : FrameView
    {
        private readonly Dictionary<Coordinate, VertexView> viewsMap = new();
        private readonly GraphFieldViewModel viewModel;
        private readonly PathfindingRangeViewModel rangeViewModel;

        private readonly CompositeDisposable disposables = new();
        private readonly CompositeDisposable vertexDisposables = new();

        public GraphFieldView(GraphFieldViewModel viewModel,
            PathfindingRangeViewModel rangeViewModel)
        {
            this.viewModel = viewModel;
            this.rangeViewModel = rangeViewModel;
            Initialize();
            this.viewModel.WhenAnyValue(x => x.Graph)
                .Where(x => x != null)
                .Do(RenderGraph)
                .Subscribe()
                .DisposeWith(disposables);
        }

        private void SubscribeOnRangeExtremumChanging(Expression<Func<PathfindingRangeViewModel, VertexModel>> expression,
            Action<VertexView> action)
        {
            rangeViewModel.WhenAnyValue(expression)
                .Where(x => x != null)
                .Do(x => action(viewsMap[x.Position]))
                .Subscribe()
                .DisposeWith(disposables);
            rangeViewModel.WhenAnyValue(expression)
               .Buffer(2, 1)
               .Where(b => b[1] == null && b[0] != null)
               .Select(b => b[0])
               .Do(x => viewsMap[x.Position].VisualizeAsRegular())
               .Subscribe()
               .DisposeWith(disposables);
        }

        private void OnTransitAdded(VertexModel vertex)
        {
            viewsMap[vertex.Position].VisualizeAsTransit();
        }

        private void OnTransitRemoved(VertexModel vertex)
        {
            viewsMap[vertex.Position].VisualizeAsRegular();
        }

        private void SubscribeOnPathfindingRangeChanging()
        {
            rangeViewModel.Transit.ActOnEveryObject(OnTransitAdded, OnTransitRemoved)
                .DisposeWith(disposables);
            SubscribeOnRangeExtremumChanging(x => x.Source, x => x.VisualizeAsSource());
            SubscribeOnRangeExtremumChanging(x => x.Target, x => x.VisualizeAsTarget());
        }

        private void RenderGraph(IGraph<VertexModel> graph)
        {
            vertexDisposables.Clear();
            RemoveAll();
            viewsMap.Clear();
            foreach (var vertex in graph)
            {
                var view = new VertexView(vertex);
                SubscribeToButton1Clicked(view);
                SubscribeToButton3Clicked(view);
                SubscribeOnWheelButton(view);
                view.DisposeWith(vertexDisposables);
                Add(view);
                viewsMap.Add(vertex.Position, view);
            }
            SubscribeOnPathfindingRangeChanging();
            SetNeedsDisplay();
        }

        private void SubscribeToButton1Clicked(VertexView view)
        {
            view.Events().MouseClick
                .Where(x => x.MouseEvent.Flags == MouseFlags.Button1Pressed)
                .InvokeCommand(rangeViewModel, x => x.AddToRangeCommand)
                .DisposeWith(vertexDisposables);

            view.Events().MouseClick
                .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.Button1Pressed)
                       && x.MouseEvent.Flags.HasFlag(MouseFlags.ButtonCtrl))
                .InvokeCommand(rangeViewModel, x => x.RemoveFromRangeCommand)
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

        private void SubscribeOnWheelButton(VertexView view)
        {
            view.Events().MouseClick
                .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.WheeledDown))
                .InvokeCommand(viewModel, x => x.DecreaseVertexCostCommand)
                .DisposeWith(vertexDisposables);
            view.Events().MouseClick
                .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.WheeledUp))
                .InvokeCommand(viewModel, x => x.IncreaseVertexCostCommand)
                .DisposeWith(vertexDisposables);
        }
    }
}
