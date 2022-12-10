using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Smoothing.Interface;
using System.Collections.Generic;
using Pathfinding.GraphLib.Smoothing;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    internal sealed class SmoothGraphMenuItem : IMenuItem
    {
        private readonly IMeanCost meanAlgorithm;
        private readonly IMessenger messenger;
        private readonly IInput<int> input;

        private Graph2D<Vertex> graph  = Graph2D<Vertex>.Empty;

        private IReadOnlyList<ISmoothLevel> SmoothLevels => ConsoleSmoothLevels.Levels;

        public int Order => 8;

        public SmoothGraphMenuItem(IMeanCost meanAlgorithm, IMessenger messenger, IInput<int> input)
        {
            this.meanAlgorithm = meanAlgorithm;
            this.messenger = messenger;
            this.messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
            this.input = input;
        }

        public bool CanBeExecuted() => graph != Graph2D<Vertex>.Empty;

        public void Execute()
        {
            var menuList = SmoothLevels.CreateMenuList();
            var message = menuList + "\nChoose smooth level: ";
            using (Cursor.CleanUpAfter())
            {
                int index = input.Input(message, SmoothLevels.Count, 1) - 1;
                var level = SmoothLevels[index];
                graph.Smooth(meanAlgorithm, level.Level);
            }
            graph.Display();
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            graph = message.Graph;
        }

        public override string ToString()
        {
            return MenuItemsNames.SmoothGraph;
        }
    }
}