using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.Shared.Extensions;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class AlgorithmRunFieldView : FrameView
    {
        private const int Speed = 140;
        private const int MaxSpeed = int.MaxValue;

        private readonly CompositeDisposable vertexDisposables = new();
        private readonly CompositeDisposable disposables = new();

        private readonly AlgorithmRunFieldViewModel viewModel;

        public AlgorithmRunFieldView(AlgorithmRunFieldViewModel viewModel,
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
            viewModel.WhenAnyValue(x => x.RunGraph)
                .DistinctUntilChanged()
                .Where(x => x is not null)
                .Do(async x => await RenderGraphState(x))
                .Subscribe()
                .DisposeWith(disposables);
            messenger.Register<OpenAlgorithmRunViewMessage>(this, OnOpen);
            messenger.Register<CloseAlgorithmRunFieldViewMessage>(this, OnCloseAlgorithmViewMessage);
            this.viewModel = viewModel;
        }

        private void OnOpen(object recipient, OpenAlgorithmRunViewMessage msg)
        {
            Visible = true;
        }

        private void OnCloseAlgorithmViewMessage(object recipient, CloseAlgorithmRunFieldViewMessage msg)
        {
            Visible = false;
        }

        private async Task RenderGraphState(IEnumerable<RunVertexModel> graphState)
        {
            Application.MainLoop.Invoke(() =>
            {
                Subviews.OfType<RunVertexView>()
                    .ToArray().ForEach(Remove);
            });
            vertexDisposables.Clear();
            var children = new List<RunVertexView>();
            await Task.Run(() =>
            {
                foreach (var vertex in graphState)
                {
                    var view = new RunVertexView(vertex);
                    SubscribeOnProcessNext(view);
                    SubscribeOnReverseNext(view);
                    children.Add(view);
                    view.DisposeWith(vertexDisposables);
                }
            });
            Application.MainLoop.Invoke(() => Add(children.ToArray()));
        }

        private void SubscribeOnProcessNext(RunVertexView view)
        {
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
        }

        private void SubscribeOnReverseNext(RunVertexView view)
        {
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
        }
    }
}
