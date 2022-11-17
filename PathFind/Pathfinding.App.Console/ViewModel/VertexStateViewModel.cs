using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using System;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(1)]
    internal sealed class VertexStateViewModel : ViewModel, IRequireIntInput, IDisposable
    {
        private readonly ConsoleVertexReverseModule reverseModule;
        private readonly ConsoleVertexChangeCostModule costModule;

        private Graph2D<Vertex> Graph { get; set; } = Graph2D<Vertex>.Empty;

        public IInput<int> IntInput { get; set; }

        private Vertex Vertex => IntInput.InputVertex(Graph);

        public VertexStateViewModel(IVertexCostFactory costFactory, ICache<Graph2D<Vertex>> cache)
        {
            Graph = cache.Cached;
            reverseModule = new ConsoleVertexReverseModule();
            costModule = new ConsoleVertexChangeCostModule(costFactory);
        }

        [MenuItem(MenuItemsNames.ReverseVertex, 0)]
        public void ReverseVertex() => reverseModule.ReverseVertex(Vertex);

        [MenuItem(MenuItemsNames.ChangeVertexCost, 1)]
        public void ChangeVertexCost() => costModule.ChangeVertexCost(Vertex);
    }
}