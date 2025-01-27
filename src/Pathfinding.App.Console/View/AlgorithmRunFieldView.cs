using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Injection;
using Pathfinding.App.Console.Messages.View;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Resources;
using Pathfinding.App.Console.ViewModel.Interface;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed partial class AlgorithmRunFieldView : FrameView
    {
        private readonly CompositeDisposable vertexDisposables = new();
        private readonly CompositeDisposable disposables = new();

        private readonly Terminal.Gui.View container = new();

        public AlgorithmRunFieldView(IAlgorithmRunFieldViewModel viewModel,
            [KeyFilter(KeyFilters.Views)] IMessenger messenger)
        {
            Visible = false;
            X = 0;
            Y = 0;
            Width = Dim.Percent(66);
            Height = Dim.Percent(95);
            Border = new Border()
            {
                BorderBrush = Color.BrightYellow,
                BorderStyle = BorderStyle.Rounded,
                Title = Resource.RunField
            };
            container.X = Pos.Center();
            container.Y = Pos.Center();
            viewModel.WhenAnyValue(x => x.RunGraph)
                .DistinctUntilChanged()
                .Where(x => x is not null)
                .Do(async x => await RenderGraphState(x))
                .Subscribe()
                .DisposeWith(disposables);
            messenger.Register<OpenAlgorithmRunViewMessage>(this, OnOpen);
            messenger.Register<CloseAlgorithmRunFieldViewMessage>(this, OnCloseAlgorithmViewMessage);
            Add(container);
        }

        private void OnOpen(object recipient, OpenAlgorithmRunViewMessage msg)
        {
            Application.MainLoop.Invoke(() => Visible = true);
        }

        private void OnCloseAlgorithmViewMessage(object recipient, CloseAlgorithmRunFieldViewMessage msg)
        {
            Application.MainLoop.Invoke(() => Visible = false);
        }

        private async Task RenderGraphState(IGraph<RunVertexModel> graph)
        {
            Application.MainLoop.Invoke(container.RemoveAll);
            vertexDisposables.Clear();
            var children = new List<RunVertexView>(graph.Count);
            await Task.Run(() =>
            {
                foreach (var vertex in graph)
                {
                    var view = new RunVertexView(vertex);
                    children.Add(view);
                    view.DisposeWith(vertexDisposables);
                }
            });
            Application.MainLoop.Invoke(() =>
            {
                container.Add(children.ToArray());
                container.Width = graph.GetWidth()
                    * GraphFieldView.DistanceBetweenVertices;
                container.Height = graph.GetLength();
            });
        }
    }
}
