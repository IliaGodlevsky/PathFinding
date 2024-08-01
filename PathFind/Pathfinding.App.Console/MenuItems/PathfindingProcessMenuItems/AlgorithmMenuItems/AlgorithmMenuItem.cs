using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Service.Interface;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems
{
    internal abstract class AlgorithmMenuItem : IMenuItem
    {
        protected readonly record struct AlgorithmInfo(
            IAlgorithmFactory<PathfindingProcess> Factory,
            string AlgorithmId,
            string StepRule = null,
            string Heuristics = null,
            int? Spread = null);

        protected readonly IMessenger messenger;

        protected abstract string LanguageKey { get; }

        protected AlgorithmMenuItem(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public virtual async Task ExecuteAsync(CancellationToken token = default)
        {
            if (token.IsCancellationRequested) return;
            var info = GetAlgorithm();
            var msg = new AlgorithmStartInfoMessage(info.Factory,
                LanguageKey, info.StepRule, info.Heuristics);
            messenger.Send(msg, Tokens.Pathfinding);
            await Task.CompletedTask;
        }

        public override string ToString()
        {
            return Languages.ResourceManager.GetString(LanguageKey);
        }

        protected abstract AlgorithmInfo GetAlgorithm();
    }
}
