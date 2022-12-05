using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
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
        private readonly IMessenger messenger;

        private Graph2D<Vertex> Graph { get; set; }

        public IInput<int> IntInput { get; set; }

        public IReadOnlyList<ISmoothLevel> SmoothLevels => ConsoleSmoothLevels.Levels;

        public GraphSmoothViewModel(IMeanCost meanAlgorithm, IMessenger messenger)
        {
            this.meanAlgorithm = meanAlgorithm;
            this.messenger = messenger;
            this.messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
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
                Graph.Smooth(meanAlgorithm, level.Level);
            }
            Graph.Display();
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            Graph = message.Graph;
        }
    }
}
