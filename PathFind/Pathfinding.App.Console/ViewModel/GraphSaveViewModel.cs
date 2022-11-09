using Pathfinding.App.Console.Attributes;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using System;

namespace Pathfinding.App.Console.ViewModel
{
    internal sealed class GraphSaveViewModel : IViewModel, IDisposable
    {
        public event Action ViewClosed;

        private readonly IGraphSerializationModule<Graph2D<Vertex>, Vertex> module;

        private IGraph<Vertex> Graph { get; set; } = Graph2D<Vertex>.Empty;

        public GraphSaveViewModel(IGraphSerializationModule<Graph2D<Vertex>, Vertex> module, ICache<Graph2D<Vertex>> graph)
        {
            this.module = module;
            Graph = graph.Cached;
        }

        public void Dispose()
        {
            ViewClosed = null;
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
            ViewClosed?.Invoke();
        }

        private bool IsGraphValid()
        {
            return Graph != Graph2D<Vertex>.Empty;
        }
    }
}