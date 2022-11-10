using Pathfinding.App.Console.Attributes;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using Shared.Primitives.Attributes;
using System;

namespace Pathfinding.App.Console.ViewModel
{
    internal sealed class GraphSaveViewModel : ViewModel, IDisposable
    {
        private readonly IGraphSerializationModule<Graph2D<Vertex>, Vertex> module;

        private IGraph<Vertex> Graph { get; set; } = Graph2D<Vertex>.Empty;

        public GraphSaveViewModel(IGraphSerializationModule<Graph2D<Vertex>, Vertex> module, 
            ICache<Graph2D<Vertex>> graph, ILog log)
            : base(log)
        {
            this.module = module;
            Graph = graph.Cached;
        }

        [Order(0)]
        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphValid))]
        [MenuItem(MenuItemsNames.SaveGraph)]
        private async void SaveGraph()
        {
            await module.SaveGraphAsync(Graph);
        }

        [Order(1)]
        [MenuItem(MenuItemsNames.Exit)]
        private void Interrupt()
        {
            RaiseViewClosed();
        }

        private bool IsGraphValid()
        {
            return Graph != Graph2D<Vertex>.Empty;
        }
    }
}