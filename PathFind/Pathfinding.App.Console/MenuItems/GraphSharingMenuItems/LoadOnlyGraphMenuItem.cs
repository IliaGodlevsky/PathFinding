using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems
{
    [LowPriority]
    internal sealed class LoadGraphOnlyMenuItem : IMenuItem
    {
        private readonly IMessenger messenger;
        private readonly IInput<string> input;
        private readonly ISerializer<IGraph<Vertex>> serializer;
        private readonly GraphsPathfindingHistory history;
        private readonly ILog log;

        public LoadGraphOnlyMenuItem(IMessenger messenger,
            IFilePathInput input,
            GraphsPathfindingHistory history,
            ISerializer<IGraph<Vertex>> serializer, ILog log)
        {
            this.history = history;
            this.serializer = serializer;
            this.messenger = messenger;
            this.input = input;
            this.log = log;
        }

        public void Execute()
        {
            try
            {
                var path = input.Input();
                var graph = serializer.DeserializeFromFile(path);
                history.Add(graph);
                if (history.Count == 1)
                {
                    var costRange = graph.First().Cost.CostRange;
                    var costMsg = new CostRangeChangedMessage(costRange);
                    messenger.Send(costMsg, Tokens.AppLayout);
                    var graphMsg = new GraphMessage(graph);
                    messenger.SendMany(graphMsg, Tokens.Visual,
                        Tokens.AppLayout, Tokens.Main, Tokens.Common);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public override string ToString()
        {
            return Languages.LoadGraphOnly;
        }
    }
}
