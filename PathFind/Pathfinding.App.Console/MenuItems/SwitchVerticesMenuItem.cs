using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.App.Console.DataAccess.UnitOfWorks;
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
        protected IUnitOfWork unitOfWork;

        protected GraphModel graph = new();
        protected InclusiveValueRange<int> xRange = default;
        protected InclusiveValueRange<int> yRange = default;

        protected SwitchVerticesMenuItem(VertexActions actions,
            IInput<ConsoleKey> keyInput,
            IUnitOfWork unitOfWork)
        {
            this.keyInput = keyInput;
            this.actions = actions;
            this.unitOfWork = unitOfWork;
        }

        public virtual bool CanBeExecuted()
        {
            return graph.Graph != Graph2D<Vertex>.Empty;
        }

        public virtual void Execute()
        {
            int x = 0, y = 0;
            var key = default(ConsoleKey);
            do
            {
                var coordinate = new Coordinate2D(x, y);
                var vertex = graph.Graph.Get(coordinate);
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
            } while (key != ConsoleKey.Escape);
            PostExecute();
        }

        protected virtual void PostExecute()
        {
            unitOfWork.UpdateGraph(graph);
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

        private void SetGraph(GraphModel graph)
        {
            this.graph = graph;
            xRange = new InclusiveValueRange<int>(graph.Graph.Width - 1);
            yRange = new InclusiveValueRange<int>(graph.Graph.Length - 1);
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterData<GraphModel>(this, Tokens.Common, SetGraph);
        }
    }
}