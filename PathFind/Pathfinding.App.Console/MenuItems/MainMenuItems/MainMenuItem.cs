using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface.Models.Read;

namespace Pathfinding.App.Console.MenuItems.MainMenuItems
{
    internal abstract class MainMenuItem<TUnit>
        : UnitDisplayMenuItem<TUnit>, IConditionedMenuItem, ICanReceiveMessage
        where TUnit : IUnit
    {
        private readonly IMessenger messenger;

        private GraphModel<Vertex> graph = null;

        protected MainMenuItem(IInput<int> input, TUnit viewModel,
            IMessenger messenger, ILog log)
            : base(input, viewModel, log)
        {
            this.messenger = messenger;
        }

        public bool CanBeExecuted()
        {
            return graph is not null;
        }

        public void RegisterHandlers(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
        }

        private void SetGraph(GraphMessage msg)
        {
            graph = msg.Graph;
        }
    }
}
