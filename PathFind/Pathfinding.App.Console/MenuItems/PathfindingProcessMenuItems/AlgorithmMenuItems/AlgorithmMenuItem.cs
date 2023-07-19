using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model.Notes;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems
{
    internal abstract class AlgorithmMenuItem : IMenuItem
    {
        protected readonly IMessenger messenger;

        protected AlgorithmMenuItem(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public virtual void Execute()
        {
            var info = GetAlgorithm();
            messenger.SendData(info, Tokens.Pathfinding);
        }

        protected abstract (IAlgorithmFactory<PathfindingProcess> Algorithm, Statistics Statistics) GetAlgorithm();
    }
}
