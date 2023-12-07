global using VertexActions = System.Collections.Generic.IReadOnlyCollection<(string ResourceName, Pathfinding.App.Console.Interface.IVertexAction Action)>;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Settings;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pathfinding.App.Console.MenuItems
{
    internal abstract class NavigateThroughVerticesMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        protected readonly IInput<ConsoleKey> keyInput;
        protected readonly VertexActions actions;

        protected IGraph<Vertex> graph = Graph<Vertex>.Empty;
        protected InclusiveValueRange<int> xRange = default;
        protected InclusiveValueRange<int> yRange = default;

        private Keys Keys { get; } = Keys.Default;

        protected NavigateThroughVerticesMenuItem(VertexActions actions,
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
                        var vertex = graph.Get(coordinate);
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
                            GetOrDefault(key)?.Invoke(vertex);
                    } while (key != Keys.ExitVerticesNavigating);
                }
            }
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

        private void SetGraph(GraphMessage msg)
        {
            graph = msg.Graph;
            xRange = new(graph.GetWidth() - 1);
            yRange = new(graph.GetLength() - 1);
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
            foreach (var action in actions)
            {
                legend.Add((action.ResourceName, (ConsoleKey)Keys[action.ResourceName]));
            }
            return legend
                .Select(l => $"{ConvertCamelCaseToRegular(l.Descriptin)} - {l.Key}")
                .Distinct()
                .CreateMenuList(columnsNumber: 3)
                .ToString();
        }

        private static string ConvertCamelCaseToRegular(string input)
        {
            var result = new StringBuilder();
            result.Append(input[0]);
            foreach (char c in input.Skip(1))
            {
                if (char.IsUpper(c))
                {
                    result.Append(' ');
                    result.Append(char.ToLower(c));
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString().Trim();
        }
    }
}