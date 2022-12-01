using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Primitives.ValueRange;
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

        public VertexStateViewModel(ICache<Graph2D<Vertex>> cache,
            ConsoleVertexChangeCostModule costModule, ConsoleVertexReverseModule reverseModule)
        {
            Graph = cache.Cached;
            this.costModule = costModule;
            this.reverseModule = reverseModule;
        }

        [MenuItem(MenuItemsNames.ReverseVertex, 0)]
        public void ReverseVertex() => reverseModule.ReverseVertex(GetInputVertex());

        [MenuItem(MenuItemsNames.ChangeVertexCost, 1)]
        public void ChangeVertexCost() => costModule.ChangeVertexCost(GetInputVertex());
    }
}