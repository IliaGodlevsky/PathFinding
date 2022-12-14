using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Coordinates;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Shared.Extensions;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems
{
    internal abstract class SwitchVerticesMenuItem : IMenuItem
    {
        protected readonly IMessenger messenger;
        protected readonly IInput<ConsoleKey> keyInput;
        protected Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        protected Dictionary<ConsoleKey, IVertexAction> Actions { get; } = new();

        public abstract int Order { get; }

        protected SwitchVerticesMenuItem(IMessenger messenger, IInput<ConsoleKey> keyInput)
        {
            this.messenger = messenger;
            this.keyInput = keyInput;
            this.messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
        }

        public virtual bool CanBeExecuted() => graph != Graph2D<Vertex>.Empty;

        public void Execute()
        {
            var xRange = new InclusiveValueRange<int>(graph.Width - 1);
            var yRange = new InclusiveValueRange<int>(graph.Length - 1);
            int x = 0, y = 0;
            var key = default(ConsoleKey);
            do
            {
                var coordinate = new Coordinate2D(x, y);
                var vertex = graph.Get(coordinate);
                Cursor.SetPosition(vertex.ConsolePosition);
                key = keyInput.Input();
                switch (key)
                {
                    case ConsoleKey.W: y = ReturnInRange(y - 1, yRange); break;
                    case ConsoleKey.S: y = ReturnInRange(y + 1, yRange); break;
                    case ConsoleKey.A: x = ReturnInRange(x - 1, xRange); break;
                    case ConsoleKey.D: x = ReturnInRange(x + 1, xRange); break;
                    default: Actions.GetOrDefault(key)?.Do(vertex); break;
                }
            } while (key != ConsoleKey.Escape);
        }

        private int ReturnInRange(int coordinate, InclusiveValueRange<int> range)
        {
            return range.ReturnInRange(coordinate, ReturnOptions.Cycle);
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            graph = message.Graph;
        }
    }
}