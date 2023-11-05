global using VertexActions = System.Collections.Generic.IReadOnlyCollection<(string ResourceName, Pathfinding.App.Console.Interface.IVertexAction Action)>;

using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Settings;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems
{
    internal abstract class SwitchVerticesMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        protected readonly IInput<ConsoleKey> keyInput;
        protected readonly VertexActions actions;

        protected IGraph<Vertex> graph = Graph<Vertex>.Empty;
        protected InclusiveValueRange<int> xRange = default;
        protected InclusiveValueRange<int> yRange = default;

        private Keys Keys { get; } = Keys.Default;

        protected SwitchVerticesMenuItem(VertexActions actions,
            IInput<ConsoleKey> keyInput)
        {
            this.keyInput = keyInput;
            this.actions = actions;
        }

        public virtual bool CanBeExecuted()
        {
            return graph != Graph<Vertex>.Empty;
        }

        public virtual void Execute()
        {
            int x = 0, y = 0;
            ConsoleKey key;
            do
            {
                var coordinate = new Coordinate(x, y);
                var vertex = graph.Get(coordinate);
                Cursor.SetPosition(vertex.ConsolePosition);
                key = keyInput.Input();
                if (key == Keys.VertexUp)
                    y = ReturnInRange(y - 1, yRange);
                else if (key == Keys.VertexDown)
                    y = ReturnInRange(y + 1, yRange);
                else if (key == Keys.VertexLeft)
                    x = ReturnInRange(x - 1, xRange);
                else if (key == Keys.VertexRight)
                    x = ReturnInRange(x + 1, xRange);
                else
                    GetOrDefault(key)?.Invoke(vertex);
            } while (key != Keys.ExitVertexSwitching);
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

        private void SetGraph(IGraph<Vertex> graph)
        {
            this.graph = graph;
            xRange = new(graph.GetWidth() - 1);
            yRange = new(graph.GetLength() - 1);
        }

        public virtual void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
        }
    }
}