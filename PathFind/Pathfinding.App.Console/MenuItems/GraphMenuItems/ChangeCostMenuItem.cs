using Autofac.Features.AttributeFilters;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    internal sealed class ChangeCostMenuItem : IMenuItem
    {
        private readonly IMessenger messenger;
        private readonly ConsoleVertexChangeCostModule costModule;
        private readonly IInput<int> input;

        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public ChangeCostMenuItem(IMessenger messenger, ConsoleVertexChangeCostModule costModule, IInput<int> input)
        {
            this.messenger = messenger;
            this.costModule = costModule;
            this.input = input;
            this.messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
        }

        public int Order => 9;

        public bool CanBeExecuted() => graph != Graph2D<Vertex>.Empty;

        public void Execute()
        {
            costModule.ChangeVertexCost(InputVertex());
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
            return "Change cost";
        }
    }
}
