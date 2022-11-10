using Pathfinding.App.Console.Attributes;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Smoothing;
using Pathfinding.GraphLib.Smoothing.Interface;
using Shared.Primitives.Attributes;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.ViewModel
{
    internal sealed class GraphSmoothViewModel : IViewModel, IRequireIntInput, IDisposable
    {
        public event Action ViewClosed;

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

        [Order(0)]
        [MenuItem(MenuItemsNames.SmoothGraph)]
        private void SmoothGraph()
        {
            graph.Smooth(costFactory, meanAlgorithm, SmoothLevel.Level);
        }

        [Order(1)]
        [MenuItem(MenuItemsNames.Exit)]
        private void Interrup()
        {
            ViewClosed?.Invoke();
        }

        public void Dispose()
        {
            ViewClosed = null;
        }
    }
}
