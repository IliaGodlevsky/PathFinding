using Pathfinding.App.Console.Attributes;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Shared.Primitives.Attributes;
using System;

namespace Pathfinding.App.Console.ViewModel
{
    internal sealed class VertexStateViewModel : IViewModel, IRequireIntInput, IDisposable
    {
        public event Action ViewClosed;

        private Graph2D<Vertex> Graph { get; set; } = Graph2D<Vertex>.Empty;

        public IInput<int> IntInput { get; set; }

        private Vertex Vertex => IntInput.InputVertex(Graph);

        public VertexStateViewModel(ICache<Graph2D<Vertex>> cache)
        {
            Graph = cache.Cached;
        }

        [Order(0)]
        [MenuItem(MenuItemsNames.ReverseVertex)]
        public void ReverseVertex()
        {
            Vertex.Reverse();
        }

        [Order(1)]
        [MenuItem(MenuItemsNames.ChangeVertexCost)]
        public void ChangeVertexCost()
        {
            Vertex.ChangeCost();
        }

        [Order(2)]
        [MenuItem(MenuItemsNames.Exit)]
        public void Interrupt()
        {
            ViewClosed?.Invoke();
        }

        public void Dispose()
        {
            ViewClosed = null;
        }
    }
}
