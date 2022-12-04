using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Smoothing;
using Pathfinding.GraphLib.Smoothing.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(1)]
    internal sealed class GraphSmoothViewModel : ViewModel, IRequireIntInput, IDisposable
    {
        private readonly IMeanCost meanAlgorithm;

        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public IInput<int> IntInput { get; set; }

        public IReadOnlyList<ISmoothLevel> SmoothLevels => ConsoleSmoothLevels.Levels;

        public GraphSmoothViewModel(IMeanCost meanAlgorithm, ICache<Graph2D<Vertex>> graph)
        {
            this.meanAlgorithm = meanAlgorithm;
            this.graph = graph.Cached;
        }

        [MenuItem(MenuItemsNames.SmoothGraph, 0)]
        private void SmoothGraph()
        {
            var menuList = SmoothLevels.CreateMenuList();
            var message = menuList + "\nChoose smooth level: ";
            using (Cursor.CleanUpAfter())
            {
                int index = IntInput.Input(message, SmoothLevels.Count, 1) - 1;
                var level = SmoothLevels[index];
                graph.Smooth(meanAlgorithm, level.Level);
            }
            graph.Display();
        }
    }
}
