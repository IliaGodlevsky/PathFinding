﻿global using VertexActions = System.Collections.Generic.IReadOnlyCollection<(string ResourceName, Pathfinding.App.Console.Interface.IVertexAction Action)>;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Settings;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems
{
    internal abstract class NavigateThroughVerticesMenuItem : IConditionedMenuItem, ICanReceiveMessage
    {
        protected readonly IInput<ConsoleKey> keyInput;
        protected readonly IRequestService<Vertex> service;
        protected readonly Lazy<VertexActions> actions;

        protected readonly HashSet<Vertex> processed = new();

        protected GraphModel<Vertex> graph = null;
        protected InclusiveValueRange<int> xRange = default;
        protected InclusiveValueRange<int> yRange = default;

        private Keys Keys { get; } = Keys.Default;

        private VertexActions Actions => actions.Value;

        protected NavigateThroughVerticesMenuItem(
            IInput<ConsoleKey> keyInput,
            IRequestService<Vertex> service)
        {
            this.keyInput = keyInput;
            this.service = service;
            actions = new(GetActions);
        }

        public virtual bool CanBeExecuted()
        {
            return graph != null;
        }

        public virtual async Task ExecuteAsync(CancellationToken token = default)
        {
            if (!token.IsCancellationRequested)
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
                await Task.CompletedTask;
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

        public virtual void RegisterHandlers(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
        }

        private string GetLegend()
        {
            return Actions
                .Select(x => (Description: x.ResourceName, Key: Keys[x.ResourceName]))
                .Concat(new (string Description, object Key)[]
                {
                    (nameof(Keys.VertexUp), Keys.VertexUp),
                    (nameof(Keys.VertexDown), Keys.VertexDown),
                    (nameof(Keys.VertexLeft), Keys.VertexLeft),
                    (nameof(Keys.VertexRight), Keys.VertexRight),
                    (nameof(Keys.ExitVerticesNavigating), Keys.ExitVerticesNavigating),
                })
                .Distinct()
                .Select(l => $"{l.Description.ConvertCamelCaseToRegular()} - {l.Key}")
                .OrderByDescending(x => x.Length)
                .CreateMenuList(columnsNumber: 3)
                .ToString();
        }
    }
}