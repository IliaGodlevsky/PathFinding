using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages.DataMessages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.MenuItems.MainMenuItems
{
    internal abstract class MainMenuItem<TUnit> : UnitDisplayMenuItem<TUnit>, IConditionedMenuItem, ICanRecieveMessage
        where TUnit : IUnit
    {
        private readonly IMessenger messenger;

        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        protected MainMenuItem(IViewFactory viewFactory, TUnit viewModel, IMessenger messenger, ILog log)
            : base(viewFactory, viewModel, log)
        {
            this.messenger = messenger;
        }

        public bool CanBeExecuted()
        {
            return graph != Graph2D<Vertex>.Empty;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, OnGraphCreated);
        }

        private void OnGraphCreated(DataMessage<Graph2D<Vertex>> msg)
        {
            graph = msg.Value;
        }
    }
}
