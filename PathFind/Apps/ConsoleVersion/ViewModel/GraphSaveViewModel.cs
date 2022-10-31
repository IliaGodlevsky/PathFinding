using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.Interface;
using ConsoleVersion.Model;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;
using System;

namespace ConsoleVersion.ViewModel
{
    internal sealed class GraphSaveViewModel : IViewModel, IDisposable
    {
        public event Action WindowClosed;

        private readonly IGraphSerializationModule<Graph2D<Vertex>, Vertex> module;

        private IGraph<Vertex> Graph { get; set; } = Graph2D<Vertex>.Empty;

        public GraphSaveViewModel(IGraphSerializationModule<Graph2D<Vertex>, Vertex> module, ICache<Graph2D<Vertex>> graph)
        {
            this.module = module;
            Graph = graph.Cached;
        }

        public void Dispose()
        {
            WindowClosed = null;
        }

        [Condition(nameof(IsGraphValid))]
        [MenuItem(MenuItemsNames.SaveGraph, 0)]
        private async void SaveGraph()
        {
            await module.SaveGraphAsync(Graph);
        }

        [MenuItem(MenuItemsNames.Exit, 1)]
        private void Interrupt()
        {
            WindowClosed?.Invoke();
        }

        private bool IsGraphValid()
        {
            return Graph != Graph2D<Vertex>.Empty;
        }
    }
}