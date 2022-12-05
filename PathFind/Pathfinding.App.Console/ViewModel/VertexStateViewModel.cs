using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using System;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(1)]
    internal sealed class VertexStateViewModel : ViewModel, IRequireIntInput, IDisposable
    {
        private readonly ConsoleVertexReverseModule reverseModule;
        private readonly IMessenger messenger;
        private readonly ConsoleVertexChangeCostModule costModule;

        private Graph2D<Vertex> Graph { get; set; } = Graph2D<Vertex>.Empty;

        public IInput<int> IntInput { get; set; }

        private Vertex InputVertex()
        {
            using (Cursor.CleanUpAfter())
            {
                return IntInput.InputVertex(Graph);
            }
        }

        public VertexStateViewModel(ConsoleVertexChangeCostModule costModule, 
            ConsoleVertexReverseModule reverseModule, IMessenger messenger)
        {
            this.costModule = costModule;
            this.reverseModule = reverseModule;
            this.messenger = messenger;
        }

        [MenuItem(MenuItemsNames.ReverseVertex, 0)]
        public void ReverseVertex() => reverseModule.ReverseVertex(InputVertex());

        [MenuItem(MenuItemsNames.ChangeVertexCost, 1)]
        public void ChangeVertexCost() => costModule.ChangeVertexCost(InputVertex());

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            Graph = message.Graph;
        }
    }
}