using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems
{
    internal abstract class RangeMenuItem : IMenuItem
    {
        protected readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;
        protected readonly IMessenger messenger;
        protected readonly IInput<int> input;
        protected Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public abstract int Order { get; }

        protected RangeMenuItem(IPathfindingRangeBuilder<Vertex> rangeBuilder, 
            IMessenger messenger, IInput<int> input)
        {
            this.rangeBuilder = rangeBuilder;
            this.messenger = messenger;
            this.input = input;
            this.messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
        }

        public abstract bool CanBeExecuted();

        public abstract void Execute();

        protected IEnumerable<Vertex> InputVertices(int number)
        {
            return input.InputVertices(graph, rangeBuilder.Range, number);
        }

        protected void IncludeInRange(Vertex vertex)
        {
            using (Cursor.UseCurrentPosition())
            {
                rangeBuilder.Include(vertex);
            }
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            graph = message.Graph;
        }
    }
}
