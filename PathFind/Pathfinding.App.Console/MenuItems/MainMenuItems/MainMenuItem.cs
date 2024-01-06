using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.MenuItems.MainMenuItems
{
    internal abstract class MainMenuItem<TUnit>
        : UnitDisplayMenuItem<TUnit>, IConditionedMenuItem, ICanRecieveMessage
        where TUnit : IUnit
    {
        private readonly IMessenger messenger;

        private GraphReadDto graph = GraphReadDto.Empty;

        protected MainMenuItem(IInput<int> input, TUnit viewModel,
            IMessenger messenger, ILog log)
            : base(input, viewModel, log)
        {
            this.messenger = messenger;
        }

        public bool CanBeExecuted()
        {
            return graph != GraphReadDto.Empty;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
        }

        private void SetGraph(GraphMessage msg)
        {
            graph = msg.Graph;
        }
    }
}
