global using VertexActions = System.Collections.Generic.IReadOnlyCollection<(string ResourceName, Pathfinding.App.Console.Interface.IVertexAction Action)>;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Settings;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems
{
    internal abstract class NavigateThroughVerticesMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        protected readonly IInput<ConsoleKey> keyInput;
        protected readonly IService service;
        protected readonly Lazy<VertexActions> actions;

        protected readonly HashSet<Vertex> processed = new();

        protected GraphReadDto graph = GraphReadDto.Empty;
        protected InclusiveValueRange<int> xRange = default;
        protected InclusiveValueRange<int> yRange = default;

        private Keys Keys { get; } = Keys.Default;

        private VertexActions Actions => actions.Value;

        protected NavigateThroughVerticesMenuItem(
            IInput<ConsoleKey> keyInput,
            IService service)
        {
            this.keyInput = keyInput;
            this.service = service;
            this.actions = new(GetActions);
        }

        public virtual bool CanBeExecuted()
        {
            return graph != GraphReadDto.Empty;
        }

        public virtual void Execute()
        {
            AppLayout.SetCursorPositionUnderGraphField();
            using (Cursor.UseCurrentPositionWithClean())
            {
                Terminal.Write(GetLegend());
                using (Cursor.UseCurrentPosition())
                {
                    int x = 0, y = 0;
                    ConsoleKey key;
                    do
                    {
                        var coordinate = new Coordinate(x, y);
                        var vertex = graph.Graph.Get(coordinate);
                        Cursor.SetPosition(vertex.ConsolePosition.Value);
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
                            Do(vertex, key);
                    } while (key != Keys.ExitVerticesNavigating);
                }
            }
        }

        protected abstract VertexActions GetActions();

        private static int ReturnInRange(int coordinate, InclusiveValueRange<int> range)
        {
            return range.ReturnInRange(coordinate, ReturnOptions.Cycle);
        }

        protected virtual void Do(Vertex vertex, ConsoleKey key)
        {
            var action = GetOrDefault(key);
            if (action is not null)
            {
                action.Invoke(vertex);
                processed.Add(vertex);
            }
        }

        private IVertexAction GetOrDefault(ConsoleKey key)
        {
            var action = Actions
                .FirstOrDefault(action => Keys.Default[action.ResourceName].Equals(key))
                .Action;
            return action;
        }

        private void SetGraph(GraphMessage msg)
        {
            graph = msg.Graph;
            xRange = new(graph.Graph.GetWidth() - 1);
            yRange = new(graph.Graph.GetLength() - 1);
        }

        public virtual void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
        }

        private string GetLegend()
        {
            var legend = new List<(string Descriptin, ConsoleKey Key)>()
            {
                (nameof(Keys.VertexUp), Keys.VertexUp),
                (nameof(Keys.VertexDown), Keys.VertexDown),
                (nameof(Keys.VertexLeft), Keys.VertexLeft),
                (nameof(Keys.VertexRight), Keys.VertexRight),
                (nameof(Keys.ExitVerticesNavigating), Keys.ExitVerticesNavigating),
            };
            foreach (var action in Actions)
            {
                legend.Add((action.ResourceName, (ConsoleKey)Keys[action.ResourceName]));
            }
            return legend
                .Select(l => $"{l.Descriptin.ConvertCamelCaseToRegular()} - {l.Key}")
                .Distinct()
                .OrderByDescending(x => x.Length)
                .CreateMenuList(columnsNumber: 3)
                .ToString();
        }
    }
}