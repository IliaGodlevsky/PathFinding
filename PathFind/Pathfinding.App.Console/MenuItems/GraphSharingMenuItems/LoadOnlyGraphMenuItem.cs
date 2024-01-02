using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
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
        private readonly ISerializer<GraphSerializationDto> serializer;
        private readonly IService service;
        private readonly ILog log;

        public LoadGraphOnlyMenuItem(IMessenger messenger,
            IFilePathInput input,
            IService service,
            ISerializer<GraphSerializationDto> serializer,
            ILog log)
        {
            this.service = service;
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
                var dto = serializer.DeserializeFromFile(path);
                // TODO: Save neighbourhood with graph
                int id = service.AddGraph(dto);
                var count = service.GetGraphIds().Count;
                if (count == 1)
                {
                    var graph = service.GetGraph(id);
                    var costRange = graph.First().Cost.CostRange;
                    var costMsg = new CostRangeChangedMessage(costRange);
                    messenger.Send(costMsg, Tokens.AppLayout);
                    var graphMsg = new GraphMessage(graph, id);
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
