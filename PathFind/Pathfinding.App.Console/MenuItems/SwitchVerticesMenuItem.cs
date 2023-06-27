using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Settings;
using Pathfinding.GraphLib.Core.Realizations.Coordinates;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems
{
    using VertexActions = IReadOnlyCollection<(string ResourceName, IVertexAction Action)>;

    internal abstract class SwitchVerticesMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        protected readonly IInput<ConsoleKey> keyInput;
        protected readonly VertexActions actions;

        protected Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;
        protected InclusiveValueRange<int> xRange = default;
        protected InclusiveValueRange<int> yRange = default;

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

        public virtual void Execute()
        {
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
                    GetOrDefault(key)?.Do(vertex);
            } while (key != Keys.Default.ResetSwitching);
        }

        private static int ReturnInRange(int coordinate, InclusiveValueRange<int> range)
        {
            return range.ReturnInRange(coordinate, ReturnOptions.Cycle);
        }

        private IVertexAction GetOrDefault(ConsoleKey key)
        {
            var action = actions
                .FirstOrDefault(action => Keys.Default[action.ResourceName].Equals(key))
                .Action;
            return action;
        }

        private void SetGraph(Graph2D<Vertex> graph)
        {
            this.graph = graph;
            xRange = new InclusiveValueRange<int>(graph.Width - 1);
            yRange = new InclusiveValueRange<int>(graph.Length - 1);
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
        }
    }
}