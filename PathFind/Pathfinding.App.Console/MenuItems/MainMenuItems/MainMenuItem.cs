using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.MenuItems.MainMenuItems
{
    internal abstract class MainMenuItem<TUnit> 
        : UnitDisplayMenuItem<TUnit>, IConditionedMenuItem, ICanRecieveMessage
        where TUnit : IUnit
    {
        private readonly IMessenger messenger;

        private IGraph<Vertex> graph = Graph<Vertex>.Empty;

        protected MainMenuItem(IInput<int> input, TUnit viewModel, 
            IMessenger messenger, ILog log)
            : base(input, viewModel, log)
        {
            this.messenger = messenger;
        }

        public bool CanBeExecuted()
        {
            return graph != Graph<Vertex>.Empty;
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
