using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
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
                Title = "Run field"
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
            Visible = true;
        }

        private void OnCloseAlgorithmViewMessage(object recipient, CloseAlgorithmRunFieldViewMessage msg)
        {
            Visible = false;
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
