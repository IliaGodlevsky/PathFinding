using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Settings;
using Pathfinding.GraphLib.Core.Realizations.Coordinates;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Shared.Extensions;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems
{
    using VertexActions = IReadOnlyDictionary<ConsoleKey, IVertexAction>;

    internal abstract class SwitchVerticesMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        protected readonly IInput<ConsoleKey> keyInput;
        protected readonly VertexActions actions;

        protected Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        protected SwitchVerticesMenuItem(VertexActions actions,
            IInput<ConsoleKey> keyInput)
        {
            this.keyInput = keyInput;
            this.actions = actions;
        }

        public virtual bool CanBeExecuted()
        {
            return graph != Graph2D<Vertex>.Empty;
        }

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
                if (key == Keys.Default.VertexUp)
                    y = ReturnInRange(y - 1, yRange);
                else if (key == Keys.Default.VertexDown)
                    y = ReturnInRange(y + 1, yRange);
                else if (key == Keys.Default.VertexLeft)
                    x = ReturnInRange(x - 1, xRange);
                else if (key == Keys.Default.VertexRight)
                    x = ReturnInRange(x + 1, xRange);
                else
                    actions.GetOrDefault(key)?.Do(vertex);
            } while (key != ConsoleKey.Escape);
        }

        private static int ReturnInRange(int coordinate, InclusiveValueRange<int> range)
        {
            return range.ReturnInRange(coordinate, ReturnOptions.Cycle);
        }

        private void SetGraph(Graph2D<Vertex> graph)
        {
            this.graph = graph;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
        }
    }
}