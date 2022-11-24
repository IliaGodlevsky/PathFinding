using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
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

        private Vertex GetInputVertex()
        {
            using (Cursor.CleanUpAfter())
            {
                return IntInput.InputVertex(Graph);
            }
        }

        public VertexStateViewModel(IVertexCostFactory costFactory,
            ICache<Graph2D<Vertex>> cache, IPathfindingRange adapter)
        {
            Graph = cache.Cached;
            reverseModule = new ConsoleVertexReverseModule(adapter);
            costModule = new ConsoleVertexChangeCostModule(costFactory);
        }

        [MenuItem(MenuItemsNames.ReverseVertex, 0)]
        public void ReverseVertex() => reverseModule.ReverseVertex(GetInputVertex());

        [MenuItem(MenuItemsNames.ChangeVertexCost, 1)]
        public void ChangeVertexCost() => costModule.ChangeVertexCost(GetInputVertex());
    }
}