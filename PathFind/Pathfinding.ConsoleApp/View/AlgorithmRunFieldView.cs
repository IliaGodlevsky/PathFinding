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
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class AlgorithmRunFieldView : FrameView
    {
        private const int Speed = 75;
        private const int MaxSpeed = int.MaxValue;

        private readonly CompositeDisposable vertexDisposables = new();
        private readonly CompositeDisposable disposables = new();

        public AlgorithmRunFieldView(AlgorithmRunFieldViewModel viewModel,
            AlgorithmRunReviewFieldViewModel reviewViewModel,
            [KeyFilter(KeyFilters.Views)] IMessenger messenger)
        {
            Visible = false;
            X = 0;
            Y = 0;
            Width = Dim.Percent(66);
            Height = Dim.Fill();
            Border = new Border()
            {
                BorderBrush = Color.BrightYellow,
                BorderStyle = BorderStyle.Rounded,
                Title = "Run field"
            };
            viewModel.WhenAnyValue(x => x.GraphState)
                .Do(x => RenderGraphState(x, viewModel))
                .Subscribe()
                .DisposeWith(disposables);
            reviewViewModel.WhenAnyValue(x => x.GraphState)
                .Do(x => RenderGraphState(x, reviewViewModel))
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

        private void RenderGraphState(IReadOnlyCollection<RunVertexModel> graphState,
            AlgorithmRunBaseViewModel viewModel)
        {
            RemoveAll();
            vertexDisposables.Clear();
            var children = new List<RunVertexView>(graphState.Count);

            foreach (var vertex in graphState)
            {
                var view = new RunVertexView(vertex);
                view.Events().MouseClick
                    .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.WheeledUp))
                    .Select(x => Speed)
                    .InvokeCommand(viewModel, x => x.ProcessNextCommand)
                    .DisposeWith(vertexDisposables);
                view.Events().MouseClick
                    .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.Button1DoubleClicked))
                    .Select(x => MaxSpeed)
                    .InvokeCommand(viewModel, x => x.ProcessNextCommand)
                    .DisposeWith(vertexDisposables);
                view.Events().MouseClick
                    .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.Button3DoubleClicked))
                    .Select(x => MaxSpeed)
                    .InvokeCommand(viewModel, x => x.ReverseNextCommand)
                    .DisposeWith(vertexDisposables);
                view.Events().MouseClick
                   .Where(x => x.MouseEvent.Flags.HasFlag(MouseFlags.WheeledDown))
                   .Select(x => Speed)
                   .InvokeCommand(viewModel, x => x.ReverseNextCommand)
                   .DisposeWith(vertexDisposables);
                children.Add(view);
                view.DisposeWith(vertexDisposables);
            }

            Application.MainLoop.Invoke(() =>
            {
                Add(children.ToArray());
            });
        }
    }
}
