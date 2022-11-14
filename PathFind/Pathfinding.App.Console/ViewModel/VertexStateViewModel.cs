using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Menu.Realizations.Attributes;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using System;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(1)]
    internal sealed class VertexStateViewModel : ViewModel, IRequireIntInput, IDisposable
    {
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
    }
}
