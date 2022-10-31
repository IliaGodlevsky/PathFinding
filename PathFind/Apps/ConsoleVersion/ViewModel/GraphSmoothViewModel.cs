using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Model;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Extensions;
using GraphLib.Realizations.Graphs;
using System;
using System.Collections.Generic;

namespace ConsoleVersion.ViewModel
{
    internal class GraphSmoothViewModel : IViewModel, IRequireIntInput, IDisposable
    {
        public event Action WindowClosed;

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

        [MenuItem(MenuItemsNames.Exit, 1)]
        private void Interrup()
        {
            WindowClosed?.Invoke();
        }

        public void Dispose()
        {
            WindowClosed = null;
        }
    }
}
