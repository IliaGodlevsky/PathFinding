using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
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
        private readonly IVertexCostFactory costFactory;

        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public IInput<int> IntInput { get; set; }

        public IReadOnlyList<ISmoothLevel> SmoothLevels => ConsoleSmoothLevels.Levels;

        public string ChooseSmoothLevelMsg { private get; set; } = string.Empty;

        private int SmoothLevelIndex => IntInput.Input(ChooseSmoothLevelMsg, SmoothLevels.Count, 1) - 1;

        private ISmoothLevel SmoothLevel => SmoothLevels[SmoothLevelIndex];

        public GraphSmoothViewModel(IMeanCost meanAlgorithm,
            IVertexCostFactory costFactory, ICache<Graph2D<Vertex>> graph)
        {
            this.costFactory = costFactory;
            this.meanAlgorithm = meanAlgorithm;
            this.graph = graph.Cached;
        }

        [MenuItem(MenuItemsNames.SmoothGraph, 0)]
        private void SmoothGraph()
        {
            graph.Smooth(costFactory, meanAlgorithm, SmoothLevel.Level);
        }
    }
}
