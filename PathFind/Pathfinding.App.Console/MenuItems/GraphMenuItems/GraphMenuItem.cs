using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    internal abstract class GraphMenuItem : IMenuItem
    {
        protected readonly IMessenger messenger;
        protected readonly IInput<int> input;

        protected GraphMenuItem(IMessenger messenger, IInput<int> input)
        {
            this.messenger = messenger;
            this.input = input;
        }

        public virtual bool CanBeExecuted() => true;

        public abstract void Execute();
    }
}
