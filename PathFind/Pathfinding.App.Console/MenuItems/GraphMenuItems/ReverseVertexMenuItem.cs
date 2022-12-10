using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    internal sealed class ReverseVertexMenuItem : IMenuItem
    {
        private readonly IMessenger messenger;
        private readonly ConsoleVertexReverseModule reverseModule;
        private readonly IInput<int> input;

        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public ReverseVertexMenuItem(IMessenger messenger, ConsoleVertexReverseModule reverseModule, IInput<int> input)
        {
            this.messenger = messenger;
            this.reverseModule = reverseModule;
            this.input = input;
            this.messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
        }

        public int Order => 10;

        public bool CanBeExecuted() => graph != Graph2D<Vertex>.Empty;

        public void Execute()
        {
            reverseModule.ReverseVertex(InputVertex());
        }

        private Vertex InputVertex()
        {
            using (Cursor.CleanUpAfter())
            {
                return input.InputVertex(graph);
            }
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            graph = message.Graph;
        }

        public override string ToString()
        {
            return "Reverse vertex";
        }
    }
}
