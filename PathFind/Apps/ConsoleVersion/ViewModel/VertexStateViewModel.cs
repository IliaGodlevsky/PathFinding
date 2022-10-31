using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Model;
using GraphLib.Realizations.Graphs;
using System;

namespace ConsoleVersion.ViewModel
{
    internal sealed class VertexStateViewModel : IViewModel, IRequireIntInput, IDisposable
    {
        public event Action WindowClosed;

        private Graph2D<Vertex> Graph { get; set; } = Graph2D<Vertex>.Empty;

        public IInput<int> IntInput { get; set; }

        private Vertex Vertex => IntInput.InputVertex(Graph);

        public VertexStateViewModel(ICache<Graph2D<Vertex>> cache)
        {
            Graph = cache.Cached;
        }

        [MenuItem(MenuItemsNames.ReverseVertex, 0)]
        public void ReverseVertex()
        {
            Vertex.Reverse();
        }

        [MenuItem(MenuItemsNames.ChangeVertexCost, 1)]
        public void ChangeVertexCost()
        {
            Vertex.ChangeCost();
        }

        [MenuItem(MenuItemsNames.Exit, 2)]
        public void Interrupt()
        {
            WindowClosed?.Invoke();
        }

        public void Dispose()
        {
            WindowClosed = null;
        }
    }
}
