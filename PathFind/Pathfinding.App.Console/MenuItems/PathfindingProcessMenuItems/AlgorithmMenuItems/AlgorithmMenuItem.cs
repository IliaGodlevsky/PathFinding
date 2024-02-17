using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems
{
    internal abstract class AlgorithmMenuItem : IMenuItem
    {
        protected readonly record struct AlgorithmInfo(
            IAlgorithmFactory<PathfindingProcess> Factory,
            RunStatisticsDto InitStatistics);

        protected readonly IMessenger messenger;

        protected abstract string LanguageKey { get; }

        protected RunStatisticsDto RunStatistics => new() { AlgorithmId = LanguageKey };

        protected AlgorithmMenuItem(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public virtual void Execute()
        {
            var info = GetAlgorithm();
            var msg = new AlgorithmStartInfoMessage(info.Factory, info.InitStatistics);
            messenger.Send(msg, Tokens.Pathfinding);
        }

        public override string ToString()
        {
            return Languages.ResourceManager.GetString(LanguageKey);
        }

        protected abstract AlgorithmInfo GetAlgorithm();
    }
}
